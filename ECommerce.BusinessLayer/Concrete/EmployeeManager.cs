using ECommerce.BusinessLayer.Abstract;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.BusinessLayer.Concrete
{
	public class EmployeeManager : IEmployeeService
	{
		private readonly IEmployeeDal _employeeDal;

		public EmployeeManager(IEmployeeDal employeeDal)
		{
			throw new NotImplementedException();
			_employeeDal = employeeDal;
		}


		public async Task TDeleteAsync(Employee t)
		{
			await _employeeDal.DeleteAsync(t);
		}

		public async Task<Employee> TGetByIdAsync(int id)
		{
			return await _employeeDal.GetByIdAsync(id);
		}

		public async Task<List<Employee>> TGetListAsync()
		{
			return await _employeeDal.GetListAsync();
		}

		public async Task TInsertAsync(Employee t)
		{
			await _employeeDal.InsertAsync(t);
		}

		public async Task TUpdateAsync(Employee t)
		{
			await _employeeDal.UpdateAsync(t);
		}
	}
}
