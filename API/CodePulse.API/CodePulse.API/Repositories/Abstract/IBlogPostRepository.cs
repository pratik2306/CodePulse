using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Abstract
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> Delete(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
    }
}
