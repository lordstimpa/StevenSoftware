using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StevenSoftware.Server.Database;
using StevenSoftware.Server.Helper;
using StevenSoftware.Server.Models;
using StevenSoftware.Server.Models.Dto;

namespace StevenSoftware.Server.Service
{
    public class BlogService
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                Content = blogPost.Content,
                CreatedAt = blogPost.CreatedAt,
                UpdatedAt = blogPost.UpdatedAt,
                Author = new UserDto
                {
                    FirstName = blogPost.Author.FirstName ?? "",
                }
            };

            return ServiceResult<BlogPostGetDto>.Ok(blogPostGetDto, "Successfully fetched blog post.");
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
                Content = post.Content,
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
            var existingBlogPost = await GetBlogPostById(blogPostDto.Id, cancellationToken);

            if (existingBlogPost != null && existingBlogPost.Data != null)
            {
                var blogPost = existingBlogPost.Data;

                if (blogPost.AuthorId != userId)
                    return ServiceResult<BlogPostGetDto>.Fail("You are not authorized to edit this blog post.");

                existingBlogPost.Data.Title = blogPostDto.Title;
                existingBlogPost.Data.Content = blogPostDto.Content;
                existingBlogPost.Data.UpdatedAt = DateTime.UtcNow;

                _dbContext.Update(existingBlogPost.Data);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ServiceResult<BlogPostGetDto>.Ok(existingBlogPost.Data, "Successfully updated blog post.");
            }
            else
            {
                var newBlogPost = new BlogPost 
                { 
                    Title = blogPostDto.Title,
                    Content = blogPostDto.Content,
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
                    Content = newBlogPost.Content,
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

            if (blogPost.AuthorId != userId)
                return ServiceResult<BlogPostGetDto>.Fail("You are not authorized to delete this blog post.");

            _dbContext.BlogPosts.Remove(blogPost);
            var affectedRow = await _dbContext.SaveChangesAsync(cancellationToken);

            if (affectedRow == 0)
                return ServiceResult<BlogPostGetDto>.Fail("Failed to delete the blog post.");

            var blogPostGetDto = new BlogPostGetDto()
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
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
