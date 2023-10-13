using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.PresentationLayer.Models
{
	public class CommentsByProductViewModel
	{
		public ResultUserDto User { get; set; }
		public string CommentText { get; set; }
	}
}
