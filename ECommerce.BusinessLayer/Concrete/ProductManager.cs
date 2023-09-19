using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Concrete
{
	public class ProductManager : IProductService
	{
		private readonly IProductDal _productDal;

		public ProductManager(IProductDal productDal)
		{
			_productDal = productDal;
		}

		public async Task TDeleteAsync(Product t)
		{
			await _productDal.DeleteAsync(t);
		}

		public async Task<Product> TGetByIdAsync(int id)
		{
			return await _productDal.GetByIdAsync(id);
		}

		public async Task<List<Product>> TGetListAsync()
		{
			return await _productDal.GetListAsync();
		}

		public async Task TInsertAsync(Product t)
		{
			await _productDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(Product t)
		{
			await _productDal.UpdateAsync(t);
		}
	}
}
