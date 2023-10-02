

using Autofac;
using Autofac.Extensions.DependencyInjection;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.BusinessLayer.DependencyResolves.Autofac;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.EntityLayer.Concrete;
using ECommerce.WebApi.Services.EmailMailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = true;
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterModule(new AutofacBusinessModule());
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(opt =>
{
	opt.AddPolicy("ModaManiaApiCors", opts =>
	{
		opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ModaManiaApiCors");
app.UseAuthorization();

app.MapControllers();

app.Run();
