namespace ECommerce.DtoLayer.Dtos.Category
{
	public record UpdateCategoryDto(int? Id, string Name, string? Description, string? ImageUrl, int? ParentCategoryId);
}
