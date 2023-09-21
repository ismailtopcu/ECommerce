using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.Concrete.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatalogSliderController : ControllerBase
	{
		private readonly ICatalogSliderService _catalogSliderService;

		public CatalogSliderController(ICatalogSliderService catalogSliderService)
		{
			_catalogSliderService = catalogSliderService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCatalogSlider()
		{
			var values = await _catalogSliderService.TGetListAsync();
			return Ok(values);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCatalogSliderById(int id)
		{
			var value = await _catalogSliderService.TGetByIdAsync(id);
			return Ok(value);
		}
		[HttpPost]
		public async Task<IActionResult> AddCatalogSlider(CatalogSlider catalogSlider)
		{
			await _catalogSliderService.TInsertAsync(catalogSlider);
			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteCatalogSlider(int id)
		{
			var value = await _catalogSliderService.TGetByIdAsync(id);
			await _catalogSliderService.TDeleteAsync(value);
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> UpdateCatalogSlider(CatalogSlider catalogSlider)
		{
			await _catalogSliderService.TUpdateAsync(catalogSlider);
			return Ok();
		}

	}
}
