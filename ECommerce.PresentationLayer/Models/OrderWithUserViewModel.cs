using ECommerce.DtoLayer.Dtos.OrderDetail;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.PresentationLayer.Models
{
    public class OrderWithUserViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
