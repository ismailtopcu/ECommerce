using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.AccountDto
{
    public class VerifyEmailDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
