using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
	public class GenericRepository<T> : IGenericDal<T> where T : class
	{
		private readonly Context _context;

		public GenericRepository(Context context)
		{
			_context = context;
		}

		public async Task DeleteAsync(T t)
		{
			_context.Remove(t);
			await _context.SaveChangesAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<List<T>> GetListAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task InsertAsync(T t)
		{
			_context.Add(t);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(T t)
		{
			_context.Update(t);
			await _context.SaveChangesAsync();
		}
	}
}
