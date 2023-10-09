using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
	public class CatalogSliderManager : ICatalogSliderService
	{
		private readonly ICatalogSliderDal _catalogSliderDal;

		public CatalogSliderManager(ICatalogSliderDal catalogSliderDal)
		{
			_catalogSliderDal = catalogSliderDal;
		}

		public async Task TDeleteAsync(CatalogSlider t)
		{
			await _catalogSliderDal.DeleteAsync(t);
		}

		public async Task<CatalogSlider> TGetByIdAsync(int id)
		{
			var value = await _catalogSliderDal.GetByIdAsync(id);
			return value;
		}

		public async Task<List<CatalogSlider>> TGetListAsync()
		{
			var values = await _catalogSliderDal.GetListAsync();
			return values;
		}

        public async Task<List<CatalogSlider>> TGetListByFilter(Expression<Func<CatalogSlider, bool>> filter)
        {
            return await _catalogSliderDal.GetListByFilter(filter);
        }

        public async Task TInsertAsync(CatalogSlider t)
		{
			await _catalogSliderDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(CatalogSlider t)
		{
			await _catalogSliderDal.UpdateAsync(t);
		}
	}
}
