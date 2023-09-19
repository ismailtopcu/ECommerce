namespace ECommerce.EntityLayer.Concrete
{
	public class Order 
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public OrderDetail OrderDetails { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime UpdatedTime { get; set; }
	}
}
