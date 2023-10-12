using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Abstract
{
	public interface IProductService : IGenericService<Product>
	{
        Task<List<ResultProductDto>> TGetProductList();
    }
}
