using ECommerce.DtoLayer.Dtos.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.Order
{
	public class CreateOrderDto
	{
       public int UserId { get; set; }
       public List<ResultOrderDetailDto>? OrderDetails { get; set; }
       public DateTime? OrderDate { get; set; }


    }
}
