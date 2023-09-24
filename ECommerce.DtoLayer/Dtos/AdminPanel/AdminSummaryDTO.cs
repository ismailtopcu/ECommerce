using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DtoLayer.Dtos.AdminPanel
{
	public class AdminSummaryDTO
	{
		public int UserCount { get; set; }
		public int ProductCount { get; set; }
		public int CategoryCount { get; set; }
		public int EmployeeCount { get; set; }
		public int OrderCount { get; set; }
	}
}
