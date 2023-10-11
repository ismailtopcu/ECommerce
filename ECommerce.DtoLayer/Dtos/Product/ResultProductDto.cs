namespace ECommerce.DtoLayer.Dtos.Product
{
	public record ResultProductDto(int Id, string Name, decimal Price, string? Description, string? Image, int Stock, int CategoryId);
	
}
