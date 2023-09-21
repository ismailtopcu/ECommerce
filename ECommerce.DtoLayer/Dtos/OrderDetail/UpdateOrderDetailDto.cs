namespace ECommerce.DtoLayer.Dtos.OrderDetail
{
	public record UpdateOrderDetailDto(int Id, int ProductId, int Quantity, decimal UnitPrice);
}
