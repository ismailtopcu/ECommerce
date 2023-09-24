using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.DtoLayer.Dtos.Employee;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeService employeeService, IMapper mapper)
		{
			_employeeService = employeeService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEmployees()
		{
			var employees = await _employeeService.TGetListAsync();
			return Ok(employees);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetEmployeeById(int id)
		{
			var employee = await _employeeService.TGetByIdAsync(id);
			return Ok(employee);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			var employee = await _employeeService.TGetByIdAsync(id);
			await _employeeService.TDeleteAsync(employee);
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> AddEmployee(CreateEmployeeDto createEmployeeDto)
		{
			var value = _mapper.Map<Employee>(createEmployeeDto); 			
			await _employeeService.TInsertAsync(value);
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
		{
			var value = _mapper.Map<Employee>(updateEmployeeDto);
			await _employeeService.TUpdateAsync(value);
			return Ok();
		}
	}
}
