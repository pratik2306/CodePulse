using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Concrete
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ImageRepository(IWebHostEnvironment environment,
            ApplicationDbContext context,   
            IHttpContextAccessor contextAccessor)
        {
            _environment = environment;
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<BlogImage>> GetAllAsync()
        {
            return await _context.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            //1 - Upload Image
            var localPath = Path.Combine(_environment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            //2-Update Database
            var httpRequest = _contextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
            blogImage.Url = urlPath;

            await _context.BlogImages.AddAsync(blogImage);
            await _context.SaveChangesAsync();

            return blogImage;
        }
    }
}
