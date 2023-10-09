using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Abstract
{
	public interface IOrderService : IGenericService<Order>
	{
        Task<List<Order>> TGetAllOrdersIncluded();

    }
}
