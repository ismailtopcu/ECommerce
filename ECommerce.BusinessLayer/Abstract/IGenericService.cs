namespace ECommerce.BusinessLayer.Abstract
{
	public interface IGenericService<T>
	{

		Task TInsertAsync(T t);
		Task TDeleteAsync(T t);
		Task TUpdateAsync(T t);
		Task<T> TGetByIdAsync(int id);
		Task<List<T>> TGetListAsync();

	}
}
