using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Abstract
{
	public interface IGenericDal<T> where T : class
	{
		Task InsertAsync(T t);
		Task DeleteAsync(T t);
		Task UpdateAsync(T t);
		Task<T> GetByIdAsync(int id);
		Task<List<T>> GetListAsync();
	}
}
