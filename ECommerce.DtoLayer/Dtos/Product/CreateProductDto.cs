using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.Product
{
	public record CreateProductDto(string Name, decimal Price, string? Description, string? Image, int Stock, int CategoryId);
}
