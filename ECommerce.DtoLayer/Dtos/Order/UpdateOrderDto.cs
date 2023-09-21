using ECommerce.DtoLayer.Dtos.OrderDetail;

namespace ECommerce.DtoLayer.Dtos.Order
{
	public record UpdateOrderDto(int Id, int UserId,decimal TotalAmount, List<UpdateOrderDetailDto> OrderDetails,DateTime OrderDate);
}
