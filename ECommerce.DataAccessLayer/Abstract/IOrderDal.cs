using ECommerce.EntityLayer.Concrete;

namespace ECommerce.DataAccessLayer.Abstract
{
	public interface IOrderDal : IGenericDal<Order>
	{
		Task<List<Order>> GetAllOrdersIncluded();
	}
}
