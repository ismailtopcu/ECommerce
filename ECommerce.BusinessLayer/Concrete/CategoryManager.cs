using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.EntityFramework;
using ECommerce.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Concrete
{
	public class CategoryManager : ICategoryService
	{
		private readonly ICategoryDal _categoryDal;

		public CategoryManager(ICategoryDal categoryDal)
		{
			_categoryDal = categoryDal;
		}

		public async Task TDeleteAsync(Category t)
		{
			await _categoryDal.DeleteAsync(t);
		}

		public async Task<Category> TGetByIdAsync(int id)
		{
			return await _categoryDal.GetByIdAsync(id);
		}

		public async Task<List<Category>> TGetListAsync()
		{
			return await _categoryDal.GetListAsync();
		}

        public async Task<List<Category>> TGetListByFilter(Expression<Func<Category, bool>> filter)
        {
            return await _categoryDal.GetListByFilter(filter);
        }

        public async Task TInsertAsync(Category t)
		{
			await _categoryDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(Category t)
		{
			await _categoryDal.UpdateAsync(t);
		}
	}
}
