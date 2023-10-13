using Autofac.Core;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.BusinessLayer.Concrete;
using ECommerce.DataAccessLayer.Abstract;
using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DataAccessLayer.EntityFramework;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.DtoLayer.Dtos.Messages;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Services;
using ECommerce.PresentationLayer.Services.RabbitMQServices;
using ECommerce.PresentationLayer.Utils.ConfigOptions;
using ECommerce.PresentationLayer.ValidationRules.MessageValidationRules;
using ECommerce.PresentationLayer.ValidationRules.UserValidationRules;
using ECommerce.PresentationLayer.ViewComponents;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IValidator<CreateNewUserDto>, CreateUserValidator>();
builder.Services.AddTransient<IValidator<CreateMessageDto>, CreateMessageValidator>();

builder.Services.Configure<GCSConfigOptions>(builder.Configuration.GetSection("GCSConfigOptions"));


builder.Services.AddIdentity<AppUser, AppRole>(opt=>
{
	opt.SignIn.RequireConfirmedEmail = true;
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IBasketService,BasketManager>();
builder.Services.AddScoped<IProductDal,EfProductDal>();
builder.Services.AddDbContext<Context>();
builder.Services.AddHttpClient();
builder.Services.AddSession(opt =>
{
	opt.IdleTimeout = TimeSpan.FromDays(15);
	opt.Cookie.HttpOnly = false;
});


builder.Services.AddSingleton(sp => new ConnectionFactory(){ Uri = new Uri(builder.Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync=true});
builder.Services.AddSingleton<RabbitMqClientService>();
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.AddHostedService<RMQWelcomeMailBackgroundService>();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("MemberPolicy", policy => policy.RequireRole("Member"));
	options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseSession();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
