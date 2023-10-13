using AutoMapper;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Category;
using ECommerce.DtoLayer.Dtos.Comment;
using ECommerce.DtoLayer.Dtos.Employee;
using ECommerce.DtoLayer.Dtos.Order;
using ECommerce.DtoLayer.Dtos.OrderDetail;
using ECommerce.DtoLayer.Dtos.Product;
using ECommerce.EntityLayer.Concrete;

namespace ECommerce.PresentationLayer.Mapping
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<CreateCategoryDto, Category>().ReverseMap();
			CreateMap<UpdateCategoryDto, Category>().ReverseMap();
			CreateMap<ResultCategoryDto, Category>().ReverseMap();

			CreateMap<CreateProductDto, Product>().ReverseMap();
			CreateMap<UpdateProductDto, Product>().ReverseMap();
			CreateMap<ResultProductDto, Product>().ReverseMap();

			CreateMap<CreateEmployeeDto, Employee>().ReverseMap();
			CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
			CreateMap<ResultEmployeeDto, Employee>().ReverseMap();

			CreateMap<CreateOrderDto, Order>().ReverseMap();
			CreateMap<UpdateOrderDto, Order>().ReverseMap();
			CreateMap<ResultOrderDto, Order>().ReverseMap();

			CreateMap<CreateOrderDetailDto, OrderDetail>().ReverseMap();
			CreateMap<UpdateOrderDetailDto, OrderDetail>().ReverseMap();
			CreateMap<ResultOrderDetailDto, OrderDetail>().ReverseMap();

			CreateMap<CreateNewUserDto, AppUser>().ReverseMap();
			CreateMap<LoginUserDto, AppUser>().ReverseMap();
			CreateMap<ResultUserDto, AppUser>().ReverseMap();

			CreateMap<CreateCommentDto, Comment>().ReverseMap();
			CreateMap<ResultCommentDto, Comment>().ReverseMap();


		}
	}
}
