using ECommerce.DtoLayer.Dtos.BasketDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
	public interface IBasketService
	{
		Task<BasketDto> AddToBasket(BasketDto basket, int productId, int quantity);
		BasketDto RemoveToBasket(BasketDto basket, int productId);
		BasketDto AddBasket(BasketDto basket, int productId);
	}
}
