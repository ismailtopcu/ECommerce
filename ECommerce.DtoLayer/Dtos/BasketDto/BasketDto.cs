using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.BasketDto
{
	public class BasketDto 
	{
		
		public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
		public decimal TotalPrice { get => BasketItems.Sum(x => x.Product.Price * x.Quantity); }
	}
}
