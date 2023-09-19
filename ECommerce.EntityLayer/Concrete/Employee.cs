namespace ECommerce.EntityLayer.Concrete
{
	public class Employee 
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string? Description { get; set; }
		public string? Image { get; set; }
		public string Title { get; set; } = null!;
	}
}
