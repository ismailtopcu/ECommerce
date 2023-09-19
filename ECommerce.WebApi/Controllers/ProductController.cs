using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GellAllProducts()
		{
			var products = await _productService.TGetListAsync();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _productService.TGetByIdAsync(id);
			return Ok(product);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _productService.TGetByIdAsync(id);
			await _productService.TDeleteAsync(product);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(CreateProductDto createProductDto)
		{
			var value = _mapper.Map<Product>(createProductDto);
			await _productService.TInsertAsync(value);
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var value = _mapper.Map<Product>(updateProductDto);
			await _productService.TUpdateAsync(value);
			return Ok();
		}
	}
}
