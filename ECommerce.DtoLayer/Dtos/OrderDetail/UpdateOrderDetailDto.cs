namespace ECommerce.DtoLayer.Dtos.OrderDetail
{
	public record UpdateOrderDetailDto(int Id,int OrderId, int ProductId, int Quantity, decimal UnitPrice);
}
