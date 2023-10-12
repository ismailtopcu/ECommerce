using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.AccountDto
{
    public class ResultUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set;}
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string? ImageUrl { get; set; }
 
    }
}
