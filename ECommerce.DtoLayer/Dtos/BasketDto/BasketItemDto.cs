using ECommerce.DtoLayer.Dtos.Product;

namespace ECommerce.DtoLayer.Dtos.BasketDto
{
	public class BasketItemDto 
	{
        public int Quantity { get; set; }
        public ResultProductDto Product { get; set; }
    }
}
