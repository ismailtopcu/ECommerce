namespace ECommerce.DtoLayer.Dtos.OrderDetail
{
	public record ResultOrderDetailDto(int Id,int OrderId ,int ProductId, int Quantity, decimal UnitPrice);
}
