using Microsoft.EntityFrameworkCore;
using StevenSoftware.Server.Database;
using StevenSoftware.Server.Helper;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Models.Dto;

namespace StevenSoftware.Server.Service
{
    public class BlogService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly MediaService _mediaService;


        public BlogService(ApplicationDbContext dbContext, MediaService mediaService)
        {
            _dbContext = dbContext;
            _mediaService = mediaService;
        }   

        public async Task<ServiceResult<BlogPostGetDto>> GetBlogPostById(int blogPostId, CancellationToken cancellationToken)
        {
            var blogPost = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);

            if (blogPost == null)
                return ServiceResult<BlogPostGetDto>.Fail("Blog post could not be found.");

            var blogPostGetDto = new BlogPostGetDto()
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

            return ServiceResult<BlogPostGetDto>.Ok(blogPostGetDto, "Successfully fetched blog post.");
        }

        public async Task<BlogPost?> GetBlogPostModelById(int blogPostId, CancellationToken cancellationToken)
        {
            return await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);
        }


        public async Task<ServiceResult<BlogPostGetListDto>> GetBlogPosts(int pageNumber, CancellationToken cancellationToken)
        {
            var totalCount = await _dbContext.BlogPosts.CountAsync(cancellationToken);
            if (totalCount <= 0)
                return ServiceResult<BlogPostGetListDto>.Fail("Blog posts could not be found.");

            var blogPosts = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber -1) * 10)
                .Take(10)
                .ToListAsync(cancellationToken);

            if (blogPosts == null)
                return ServiceResult<BlogPostGetListDto>.Fail("Blog posts could not be found.");

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

            var blogPostGetListDto = new BlogPostGetListDto
            {
                BlogPosts = blogPostDtos,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
            };

            return ServiceResult<BlogPostGetListDto>.Ok(blogPostGetListDto, "Successfully fetched blog posts.");
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
                return ServiceResult<BlogPostGetDto>.Fail("Blog post could not be found.");

            var fileName = Path.GetFileName(blogPost.CoverImage);
            if (!string.IsNullOrEmpty(fileName))
                _mediaService.DeleteMediaByFileName(fileName);

            _dbContext.BlogPosts.Remove(blogPost);
            var affectedRow = await _dbContext.SaveChangesAsync(cancellationToken);

            if (affectedRow == 0)
                return ServiceResult<BlogPostGetDto>.Fail("Failed to delete the blog post.");

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
    }
}
