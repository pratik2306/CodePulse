using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Abstract;
using CodePulse.API.Repositories.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;

        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDTO request)
        {
            List<Category> categories = new List<Category>();
            foreach (var item in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(item);
                if (existingCategory != null)
                    categories.Add(existingCategory);
            }

            var blogPost = _mapper.Map<BlogPost>(request);
            blogPost.Categories = categories;
            var result = await _blogPostRepository.CreateAsync(blogPost);
            return Ok(_mapper.Map<BlogPostDTO>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();
            return Ok(_mapper.Map<List<BlogPostDTO>>(blogPosts));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost is null)
                return NotFound();

            return Ok(_mapper.Map<BlogPostDTO>(blogPost));

        }

        [HttpGet]
        [Route("{urlhandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandl(string urlhandle)
        {
            var blogPost = await _blogPostRepository.GetByUrlHandleAsync(urlhandle);
            if (blogPost is null)
                return NotFound();

            return Ok(_mapper.Map<BlogPostDTO>(blogPost));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id, UpdateBlogPostRequestDTO request)
        {
            List<Category> categories = new List<Category>();
            foreach (var item in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetByIdAsync(item);
                if (existingCategory != null)
                    categories.Add(existingCategory);
            }

            var blogPost = _mapper.Map<BlogPost>(request);
            blogPost.Id = id;
            blogPost.Categories = categories;

            var result = await _blogPostRepository.UpdateAsync(blogPost);
            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<BlogPostDTO>(result));

        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var result = await _blogPostRepository.Delete(id);
            if (result is null)
                return NotFound();

            return Ok(_mapper.Map<BlogPostDTO>(result));
        }
    }
}
