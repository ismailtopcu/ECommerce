using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Concrete
{
	public class OrderDetailManager : IOrderDetailService
	{
		private readonly IOrderDetailDal _orderDetailDal;

		public OrderDetailManager(IOrderDetailDal orderDetailDal)
		{
			_orderDetailDal = orderDetailDal;
		}

		public async Task TDeleteAsync(OrderDetail t)
		{
			await _orderDetailDal.DeleteAsync(t);
		}

		public async Task<OrderDetail> TGetByIdAsync(int id)
		{
			return await _orderDetailDal.GetByIdAsync(id);
		}

		public async Task<List<OrderDetail>> TGetListAsync()
		{
			return await _orderDetailDal.GetListAsync();
		}

		public async Task TInsertAsync(OrderDetail t)
		{
			await _orderDetailDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(OrderDetail t)
		{
			await _orderDetailDal.UpdateAsync(t);
		}
	}
}
