namespace ECommerce.DtoLayer.Dtos.Employee
{
	public record UpdateEmployeeDto(int Id, string Name, string Surname, string? Description, string? Image, string Title);
}
