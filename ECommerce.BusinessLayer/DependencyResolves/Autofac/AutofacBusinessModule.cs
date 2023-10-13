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

			builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();
			builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();

			builder.RegisterType<EfProductDal>().As<IProductDal>().InstancePerLifetimeScope();
			builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();

			builder.RegisterType<EfOrderDal>().As<IOrderDal>().InstancePerLifetimeScope();
			builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();

			builder.RegisterType<EfOrderDetailDal>().As<IOrderDetailDal>().InstancePerLifetimeScope();
			builder.RegisterType<OrderDetailManager>().As<IOrderDetailService>().InstancePerLifetimeScope();

			builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>().InstancePerLifetimeScope();
			builder.RegisterType<EmployeeManager>().As<IEmployeeService>().InstancePerLifetimeScope();

            builder.RegisterType<EfMessageDal>().As<IMessageDal>().InstancePerLifetimeScope();
            builder.RegisterType<MessageManager>().As<IMessageService>().InstancePerLifetimeScope();

            builder.RegisterType<EfCommentDal>().As<ICommentDal>().InstancePerLifetimeScope();
            builder.RegisterType<CommentManager>().As<ICommentService>().InstancePerLifetimeScope();




        }
    }
}
