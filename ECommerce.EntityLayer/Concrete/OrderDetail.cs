using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EntityLayer.Concrete
{
	public class OrderDetail
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
