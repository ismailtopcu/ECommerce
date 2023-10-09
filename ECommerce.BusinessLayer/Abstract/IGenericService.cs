using System.Linq.Expressions;

namespace ECommerce.BusinessLayer.Abstract
{
	public interface IGenericService<T>
	{

		Task TInsertAsync(T t);
		Task TDeleteAsync(T t);
		Task TUpdateAsync(T t);
		Task<T> TGetByIdAsync(int id);
		Task<List<T>> TGetListAsync();
        Task<List<T>> TGetListByFilter(Expression<Func<T, bool>> filter);


    }
}
