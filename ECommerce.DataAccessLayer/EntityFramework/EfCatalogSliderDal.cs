using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.EntityLayer.Concrete.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.EntityFramework
{
	public class EfCatalogSliderDal : GenericRepository<CatalogSlider>, ICatalogSliderDal
	{
		public EfCatalogSliderDal(Context context) : base(context)
		{
		}
	}
}
