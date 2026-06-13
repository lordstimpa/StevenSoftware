using Microsoft.EntityFrameworkCore;
using StevenSoftware.Server.Database;
using StevenSoftware.Server.Helper;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace StevenSoftware.Server.Service
{
    public class BlogService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MediaService _mediaService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<BlogService> _logger;

        public BlogService(ApplicationDbContext dbContext, MediaService mediaService, IMemoryCache cache, ILogger<BlogService> logger)
        {
            _dbContext = dbContext;
            _mediaService = mediaService;
            _cache = cache;
            _logger = logger;
        }

        private const string BlogListCacheKey = "blogposts_list";
        private const string BlogPostCacheKeyPrefix = "blogpost_";

        public async Task<ServiceResult<BlogPostGetDto>> GetBlogPostById(int blogPostId, CancellationToken cancellationToken)
        {
            var cacheKey = $"{BlogPostCacheKeyPrefix}{blogPostId}";

            if (_cache.TryGetValue(cacheKey, out BlogPostGetDto cached))
                return ServiceResult<BlogPostGetDto>.Ok(cached, "Fetched from cache.");

            var blogPost = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);

            if (blogPost == null)
                return ServiceResult<BlogPostGetDto>.Fail("Blog post could not be found.");

            var dto = new BlogPostGetDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Summary = blogPost.Summary,
                Content = blogPost.Content,
                CoverImage = blogPost.CoverImage,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                Author = new UserDto
                {
                    FirstName = blogPost.Author.FirstName ?? "",
                }
            };

            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(10));

            return ServiceResult<BlogPostGetDto>.Ok(dto, "Fetched from DB.");
        }

        public async Task<BlogPost?> GetBlogPostModelById(int blogPostId, CancellationToken cancellationToken)
        {
            return await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);
        }


        public async Task<ServiceResult<BlogPostGetListDto>> GetBlogPosts(int pageNumber, CancellationToken cancellationToken)
        {
            var cacheKey = $"{BlogListCacheKey}_page_{pageNumber}";

            if (_cache.TryGetValue(cacheKey, out BlogPostGetListDto cached))
                return ServiceResult<BlogPostGetListDto>.Ok(cached, "Fetched from cache.");

            var totalCount = await _dbContext.BlogPosts.CountAsync(cancellationToken);

            if (totalCount <= 0)
                return ServiceResult<BlogPostGetListDto>.Fail("Blog posts could not be found.");

            var blogPosts = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * 10)
                .Take(10)
                .ToListAsync(cancellationToken);

            var blogPostDtos = blogPosts.Select(post => new BlogPostGetDto
            {
                Id = post.Id,
                Title = post.Title,
                Summary = post.Summary,
                Content = post.Content,
                CoverImage = post.CoverImage,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                Author = new UserDto
                {
                    FirstName = post.Author.FirstName ?? "",
                }
            }).ToList();

            var result = new BlogPostGetListDto
            {
                BlogPosts = blogPostDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
            };

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

            return ServiceResult<BlogPostGetListDto>.Ok(result, "Fetched from DB.");
        }

        public async Task<ServiceResult<BlogPostGetDto>> UpdateBlogPost(BlogPostUpdateDto blogPostDto, string userId, CancellationToken cancellationToken)
        {
            var existingBlogPost = await GetBlogPostModelById(blogPostDto.Id, cancellationToken);

            if (existingBlogPost != null)
            {
                if (string.IsNullOrEmpty(blogPostDto.CoverImage))
                {
                    var fileName = Path.GetFileName(existingBlogPost.CoverImage);
                    if (!string.IsNullOrEmpty(fileName))
                        _mediaService.DeleteMediaByFileName(fileName);
                }

                existingBlogPost.Title = blogPostDto.Title;
                existingBlogPost.Summary = blogPostDto.Summary;
                existingBlogPost.Content = blogPostDto.Content;
                existingBlogPost.CoverImage = blogPostDto.CoverImage;
                existingBlogPost.UpdatedAt = DateTime.UtcNow;

                _dbContext.Update(existingBlogPost);
                await _dbContext.SaveChangesAsync(cancellationToken);
                InvalidateBlogCache(existingBlogPost.Id);

                var blogPostGetDto = new BlogPostGetDto()
                {
                    Id = existingBlogPost.Id,
                    Title = existingBlogPost.Title,
                    Summary = existingBlogPost.Summary,
                    Content = existingBlogPost.Content,
                    CoverImage = existingBlogPost.CoverImage,
                    CreatedAt = existingBlogPost.CreatedAt,
                    UpdatedAt = existingBlogPost.UpdatedAt,
                    Author = new UserDto
                    {
                        FirstName = existingBlogPost.Author?.FirstName ?? "",
                    }
                };

                return ServiceResult<BlogPostGetDto>.Ok(new BlogPostGetDto(), "Successfully updated blog post.");
            }
            else
            {
                var newBlogPost = new BlogPost 
                { 
                    Title = blogPostDto.Title,
                    Summary = blogPostDto.Summary,
                    Content = blogPostDto.Content,
                    CoverImage = blogPostDto.CoverImage,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    AuthorId = userId
                };

                _dbContext.BlogPosts.Add(newBlogPost);
                await _dbContext.SaveChangesAsync(cancellationToken);
                InvalidateBlogCache(newBlogPost.Id);

                var blogPostGetDto = new BlogPostGetDto()
                {
                    Id = newBlogPost.Id,
                    Title = newBlogPost.Title,
                    Summary = newBlogPost.Summary,
                    Content = newBlogPost.Content,
                    CoverImage = newBlogPost.CoverImage,
                    CreatedAt = newBlogPost.CreatedAt,
                    UpdatedAt = newBlogPost.UpdatedAt,
                    AuthorId = newBlogPost.AuthorId,
                };

                return ServiceResult<BlogPostGetDto>.Ok(blogPostGetDto, "Successfully created blog post.");
            }
        }

        public async Task<ServiceResult<BlogPostGetDto>> DeleteBlogPost(int blogPostId, string userId, CancellationToken cancellationToken)
        {
            var blogPost = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);

            if (blogPost == null)
            {
                _logger.LogWarning("Blog post with ID {BlogPostId} not found", blogPostId);
                return ServiceResult<BlogPostGetDto>.Fail("Blog post could not be found.");
            }

            var fileName = Path.GetFileName(blogPost.CoverImage);
            if (!string.IsNullOrEmpty(fileName))
                _mediaService.DeleteMediaByFileName(fileName);

            _dbContext.BlogPosts.Remove(blogPost);
            var affectedRow = await _dbContext.SaveChangesAsync(cancellationToken);
            InvalidateBlogCache(blogPost.Id);

            if (affectedRow == 0)
            {
                _logger.LogWarning("Failed to delete blog post with ID {BlogPostId}", blogPostId);
                return ServiceResult<BlogPostGetDto>.Fail("Failed to delete the blog post.");
            }

            var blogPostGetDto = new BlogPostGetDto()
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Summary = blogPost.Summary,
                Content = blogPost.Content,
                CoverImage = blogPost.CoverImage,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                AuthorId = blogPost.AuthorId,
                Author = new UserDto
                {
                    FirstName = blogPost.Author.FirstName ?? "",
                    LastName = blogPost.Author.LastName ?? "",
                    Email = blogPost.Author.Email ?? ""
                }
            };

            return ServiceResult<BlogPostGetDto>.Ok(blogPostGetDto, "Successfully deleted blog post.");
        }

        private void InvalidateBlogCache(int? blogPostId = null)
        {
            for (int i = 1; i <= 50; i++)
            {
                _cache.Remove($"{BlogListCacheKey}_page_{i}");
            }

            if (blogPostId.HasValue)
            {
                _cache.Remove($"{BlogPostCacheKeyPrefix}{blogPostId}");
            }
        }
    }
}
