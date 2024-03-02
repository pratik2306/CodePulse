using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/categories
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;

        }

        [Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> CreateCategoy(CreateCategoryRequestDTO request)
        {
            var category = await _categoryRepository.CreateAsync(_mapper.Map<Category>(request));
            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(_mapper.Map<List<CategoryDTO>>(categories));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //GET: https://localhost:7777/api/categories/{id}
        public async Task<ActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
                return NotFound();

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> UpdateCategory([FromRoute] Guid id,[FromBody] UpdateCategoryRequestDto request)
        {
            var category = _mapper.Map<Category>(request);
            category.Id = id;

            var result = await _categoryRepository.UpdateAsync(category);

            if(result is null) return NotFound();

            return Ok(_mapper.Map<CategoryDTO>(result));
        }

        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category is null) return NotFound();
            return Ok(_mapper.Map<CategoryDTO>(category));
        }
    }
}
