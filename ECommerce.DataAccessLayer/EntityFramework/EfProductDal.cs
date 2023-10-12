using AutoMapper;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccessLayer.EntityFramework
{
	public class EfProductDal : GenericRepository<Product>, IProductDal
	{
		private readonly Context _context;
		private readonly IMapper _mapper;
        public EfProductDal(Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultProductDto>> GetProductList()
		{
			var values = await _context.Products.Include(x => x.Category).ToListAsync();
			var result = _mapper.Map<List<ResultProductDto>>(values);
			return result;
		}

		public async Task<List<ResultProductDto>> GetSearchedProductList(string searchTerm)
		{
			var query = _context.Products.Include(x => x.Category).AsQueryable();
			List<Product> products;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				products = await query.ToListAsync(); // Tüm ürünleri çekiyoruz
				products = products
					.Where(x => x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
					.ToList(); // Ardından istemci tarafında filtreleme yapılıyor
			}
			else
			{
				products = await query.ToListAsync(); // Eğer searchTerm boşsa, tüm ürünleri çekiyoruz
			}

			var result = _mapper.Map<List<ResultProductDto>>(products);
			return result;
		}

	}


}
