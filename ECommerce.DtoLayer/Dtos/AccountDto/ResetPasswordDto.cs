using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.AccountDto
{
	public class ResetPasswordDto
	{
		public string UserName { get; set; }
		public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
		public string Token { get; set; }

	}
}
