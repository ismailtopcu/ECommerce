using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EntityLayer.Concrete
{
	public class Category 
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; } 
		public string? ImageUrl { get; set; }
		public int? ParentCategoryId  { get; set; }
		public Category ParentCategory { get; set; }
		public List<Product> Products { get; set; }
	}
}
