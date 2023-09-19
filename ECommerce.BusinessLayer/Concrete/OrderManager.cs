using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Concrete
{
	public class OrderManager : IOrderService
	{
		private readonly IOrderDal _orderDal;

		public OrderManager(IOrderDal orderDal)
		{
			_orderDal = orderDal;
		}

		public async Task TDeleteAsync(Order t)
		{
			await _orderDal.DeleteAsync(t);
		}

		public async Task<Order> TGetByIdAsync(int id)
		{
			return await _orderDal.GetByIdAsync(id);
		}

		public async Task<List<Order>> TGetListAsync()
		{
			return await _orderDal.GetListAsync();
		}

		public async Task TInsertAsync(Order t)
		{
			await _orderDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(Order t)
		{
			await _orderDal.UpdateAsync(t);
		}
	}
}
