namespace ECommerce.DtoLayer.Dtos.Category
{
	public class ResultCategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public int? ParentCategoryId { get; set; }

	}
}
