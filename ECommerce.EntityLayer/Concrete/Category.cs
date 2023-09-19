namespace ECommerce.EntityLayer.Concrete
{
	public class Category 
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string? Image { get; set; }
		public bool Status { get; set; }
		public List<Product> Products { get; set; }
	}
}
