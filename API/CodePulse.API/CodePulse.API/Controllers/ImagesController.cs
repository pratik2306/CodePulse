using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImagesController(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
           var images = await _imageRepository.GetAllAsync();
           return Ok(_mapper.Map<List<BlogImageDTO>>(images));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file,
            [FromForm] string fileName, [FromForm] string title)
        {
            validateFileUpload(file);
            
            if(ModelState.IsValid)
            {
                BlogImage image = new BlogImage()
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };
                image = await _imageRepository.Upload(file,image);

                return Ok(_mapper.Map<BlogImageDTO>(image));
            }

            return BadRequest(ModelState);
        }

        private void validateFileUpload(IFormFile file) {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
                ModelState.AddModelError("file", "Unsupported file format");
            if (file.Length > 10485760)
                ModelState.AddModelError("file", "File size can noot be more then 10MB");
        }
    }
}
