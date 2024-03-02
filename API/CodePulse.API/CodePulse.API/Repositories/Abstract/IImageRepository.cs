﻿using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Abstract
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        Task<IEnumerable<BlogImage>> GetAllAsync();
    }
}
