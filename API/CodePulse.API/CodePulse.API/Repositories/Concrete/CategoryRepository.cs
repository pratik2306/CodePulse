using AutoMapper;
using Azure.Core;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCategory is not null)
            {
                _context.Categories.Remove(existingCategory);
                _context.SaveChanges();
            }

            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.Where(x=> x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
           var existingCategory = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == category.Id);
           
            if(existingCategory is not null)
            {
                existingCategory.UrlHandle = category.UrlHandle;
                existingCategory.Name   = category.Name;
                _context.SaveChanges();
            }

            return existingCategory;
        }
    }
}
