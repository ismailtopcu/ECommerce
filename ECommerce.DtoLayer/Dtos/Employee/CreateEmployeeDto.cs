using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.Employee
{
	public record CreateEmployeeDto(int Id, string Name, string Surname, string? Description, string? Image, string Title);
}
