using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.EntityLayer.Concrete
{
	public class Order 
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public decimal? TotalAmount { get; set; }
		public List<OrderDetail>? OrderDetails { get; set; }
		public DateTime? OrderDate{ get; set; }
		public bool OrderFinished { get; set;}
	}
}
