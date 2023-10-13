using Microsoft.AspNetCore.Identity;

namespace ECommerce.EntityLayer.Concrete
{
	public class AppUser : IdentityUser<int>
	{
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;
		public string? City { get; set; } = null!;
		public string? ImageUrl { get; set; }
		public int? ConfirmCode { get; set; }
		public List<Comment> Comments { get; set; }
	}
}
