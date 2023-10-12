using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.DataAccessLayer.Abstract
{
	public interface IProductDal : IGenericDal<Product>
	{
		Task<List<ResultProductDto>> GetProductList();
		Task<List<ResultProductDto>> GetSearchedProductList(string searchTerm);

	}
}
