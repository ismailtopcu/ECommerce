using System.ComponentModel.DataAnnotations;

namespace ECommerce.DtoLayer.Dtos.AccountDto
{
	public class LoginUserDto
	{
		[Required(ErrorMessage = "Kullanıcı Adını Giriniz")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Şifrenizi Giriniz")]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
