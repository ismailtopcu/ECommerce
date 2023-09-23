using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.AccountDto
{
    public class UpdatePasswordDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
