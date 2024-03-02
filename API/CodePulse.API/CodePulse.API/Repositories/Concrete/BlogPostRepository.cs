using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Concrete
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            await _context.SaveChangesAsync();

            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.Include(x=> x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _context.BlogPosts.Where(x=> x.Id == id).Include(x => x.Categories).FirstOrDefaultAsync();
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _context.BlogPosts.Include(x => x.Categories).Where(x=>x.Id == blogPost.Id).FirstOrDefaultAsync();
            if(existingBlogPost is not null)
            {
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.Categories = blogPost.Categories;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.IsVisible = blogPost.IsVisible;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
            }
            await _context.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost?> Delete(Guid id)
        {
            var existingBlogPost = await _context.BlogPosts.Include(x => x.Categories).Where(x => x.Id == id).FirstOrDefaultAsync();

            if(existingBlogPost is not null)
            {
                _context.Remove(existingBlogPost);
               await  _context.SaveChangesAsync();
            }

            return existingBlogPost;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _context.BlogPosts.Where(x => x.UrlHandle == urlHandle).Include(x => x.Categories).FirstOrDefaultAsync();
        }
    }
}
