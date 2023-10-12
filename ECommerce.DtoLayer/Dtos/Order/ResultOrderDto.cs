using ECommerce.DtoLayer.Dtos.OrderDetail;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.DtoLayer.Dtos.Order
{
    public class ResultOrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<ResultOrderDetailDto>? OrderDetails { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool OrderFinished { get; set; }

    }
}
