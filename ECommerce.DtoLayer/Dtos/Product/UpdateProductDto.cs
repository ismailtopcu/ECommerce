namespace ECommerce.DtoLayer.Dtos.Product
{
	public record UpdateProductDto(int Id, string Name, decimal Price, string? Description, string? Image, int Stock, int CategoryId);
}
