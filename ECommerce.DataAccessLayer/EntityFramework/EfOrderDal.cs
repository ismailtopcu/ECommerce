using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccessLayer.EntityFramework
{
	public class EfOrderDal : GenericRepository<Order>, IOrderDal
	{
        private readonly Context _context;
		public EfOrderDal(Context context): base(context)
		{
            _context = context;
		}

        public async Task<List<Order>> GetAllOrdersIncluded()
        {
            return await _context.Orders.Include(x=>x.OrderDetails).ToListAsync();
        }
    }


}
