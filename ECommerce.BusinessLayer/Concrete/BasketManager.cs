using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.BasketDto;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
	public class BasketManager : IBasketService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IMapper _mapper;

		public BasketManager(IHttpClientFactory httpClientFactory, IMapper mapper)
		{
			_httpClientFactory = httpClientFactory;
			_mapper = mapper;
		}

		public async Task<BasketDto> AddToBasket(BasketDto basket, int productId, int quantity)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7175/api/Product/GetProductById/" + productId);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var product = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);
				if (product != null)
				{
					var existingItem = basket.BasketItems.FirstOrDefault(item => item.Product.Id == productId);

					if (existingItem != null)
					{
						existingItem.Quantity += quantity;
					}
					else
					{
						var basketItem = new BasketItemDto
						{
							Product = product,
							Quantity = quantity
						};

						basket.BasketItems.Add(basketItem);
					}
				}
			}
			return basket;
			
		}

		public BasketDto RemoveToBasket(BasketDto basket, int productId)
		{
			var value = basket.BasketItems.Where(x=>x.Product.Id == productId).FirstOrDefault();
			if (value != null)
			{
				if(value.Quantity>1)
				{
					value.Quantity -= 1;					
				}
				else
				{
					basket.BasketItems.Remove(value);
				}
			}
			return basket;
		}
		public BasketDto AddBasket(BasketDto basket, int productId)
		{
			var value = basket.BasketItems.Where(x=>x.Product.Id == productId).FirstOrDefault();
			if (value != null)
			{
				if(value.Quantity>0)
				{
					value.Quantity += 1;					
				}
				else
				{
					basket.BasketItems.Add(value);
				}
			}
			return basket;
		}

	}
}
