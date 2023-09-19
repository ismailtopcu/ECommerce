using ECommerce.DtoLayer.Dtos.OrderDetail;

namespace ECommerce.DtoLayer.Dtos.Order
{
	public record UpdateOrderDto(int Id, List<UpdateOrderDetailDto> OrderDetails);
}
