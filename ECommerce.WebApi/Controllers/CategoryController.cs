using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var values = await _categoryService.TGetListAsync();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var value = await _categoryService.TGetByIdAsync(id);
			return Ok(value);
		}

        [HttpPost]
		public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
		{
			var value = _mapper.Map<Category>(createCategoryDto);
			await _categoryService.TInsertAsync(value);
			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var value = await _categoryService.TGetByIdAsync(id);
			await _categoryService.TDeleteAsync(value);
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			var value = _mapper.Map<Category>(updateCategoryDto);
			await _categoryService.TUpdateAsync(value);
			return Ok();
		}
	}
}
