using Autofac;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.DependencyResolves.Autofac
{
	public class AutofacBusinessModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{

			builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
			builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

			builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
			builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();

			builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();
			builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();

			builder.RegisterType<EfOrderDetailDal>().As<IOrderDetailDal>().SingleInstance();
			builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>().SingleInstance();

			builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>().SingleInstance();
			builder.RegisterType<EmployeeManager>().As<IEmployeeService>().SingleInstance();
		}
	}
}
