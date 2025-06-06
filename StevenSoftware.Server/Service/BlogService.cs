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

        public BlogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public async Task<ServiceResult<BlogPost>> GetBlogPostById(int blogPostId, CancellationToken cancellationToken)
        {
            var blogPost = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);

            if (blogPost == null)
                return ServiceResult<BlogPost>.Fail("Blog post could not be found.");

            return ServiceResult<BlogPost>.Ok(blogPost);
        }

        public async Task<ServiceResult<List<BlogPost>>> GetBlogPosts(int pageNumber, CancellationToken cancellationToken)
        {
            var blogPosts = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber -1) * 10)
                .Take(10)
                .ToListAsync(cancellationToken);

            if (blogPosts == null)
                return ServiceResult<List<BlogPost>>.Fail("Blog posts could not be found.");

            return ServiceResult<List<BlogPost>>.Ok(blogPosts);
        }

        public async Task<ServiceResult<BlogPost>> UpdateBlogPost(BlogPostDto blogPostDto, string userId, CancellationToken cancellationToken)
        {
            var existingBlogPost = await GetBlogPostById(blogPostDto.Id, cancellationToken);

            if (existingBlogPost != null && existingBlogPost.Data != null)
            {
                var blogPost = existingBlogPost.Data;

                if (blogPost.AuthorId != userId)
                    return ServiceResult<BlogPost>.Fail("You are not authorized to edit this blog post.");

                existingBlogPost.Data.Title = blogPostDto.Title;
                existingBlogPost.Data.Content = blogPostDto.Content;
                existingBlogPost.Data.UpdatedAt = DateTime.Now;

                _dbContext.Update(existingBlogPost);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ServiceResult<BlogPost>.Ok(existingBlogPost.Data);
            }
            else
            {
                var newBlogPost = new BlogPost 
                { 
                    Title = blogPostDto.Title,
                    Content = blogPostDto.Content,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    AuthorId = userId
                };

                _dbContext.BlogPosts.Add(newBlogPost);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return ServiceResult<BlogPost>.Ok(newBlogPost);
            }
        }

        public async Task<ServiceResult<BlogPost>> DeleteBlogPost(int blogPostId, string userId, CancellationToken cancellationToken)
        {
            var blogPost = await _dbContext.BlogPosts
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == blogPostId, cancellationToken);

            if (blogPost == null)
                return ServiceResult<BlogPost>.Fail("Blog post could not be found.");

            if (blogPost.AuthorId != userId)
                return ServiceResult<BlogPost>.Fail("You are not authorized to delete this blog post.");

            _dbContext.BlogPosts.Remove(blogPost);
            var affectedRow = await _dbContext.SaveChangesAsync(cancellationToken);

            if (affectedRow == 0)
                return ServiceResult<BlogPost>.Fail("Failed to delete the blog post.");

            return ServiceResult<BlogPost>.Ok(blogPost);
        }
    }
}
