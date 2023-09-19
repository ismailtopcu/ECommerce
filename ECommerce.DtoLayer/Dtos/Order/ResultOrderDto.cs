using ECommerce.DtoLayer.Dtos.OrderDetail;

namespace ECommerce.DtoLayer.Dtos.Order
{
	public record ResultOrderDto(int Id, List<UpdateOrderDetailDto> OrderDetails);
}
