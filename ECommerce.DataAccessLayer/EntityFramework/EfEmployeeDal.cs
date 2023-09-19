using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.DataAccessLayer.EntityFramework
{
	public class EfEmployeeDal : GenericRepository<Employee>, IEmployeeDal
	{
		public EfEmployeeDal(Context context) : base(context)
		{
		}
	}


}
