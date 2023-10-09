using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.Concrete
{
	public class Product 
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public decimal Price { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public int Stock { get; set; }
		public int CategoryID { get; set; }
		public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
