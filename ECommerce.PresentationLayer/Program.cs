using ECommerce.DataAccessLayer.Concrete;
using ECommerce.DtoLayer.Dtos.AccountDto;
using ECommerce.EntityLayer.Concrete;
using ECommerce.PresentationLayer.Services;
using ECommerce.PresentationLayer.ValidationRules.UserValidationRules;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IValidator<CreateNewUserDto>, CreateUserValidator>();

builder.Services.AddIdentity<AppUser, AppRole>(opt=>
{
	opt.SignIn.RequireConfirmedEmail = true;
    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>>();
builder.Services.AddScoped<ApiService>();
builder.Services.AddDbContext<Context>();
builder.Services.AddHttpClient();


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

//builder.Services.ConfigureApplicationCookie(options =>
//{
//	options.Cookie.HttpOnly = false;
//	options.ExpireTimeSpan = TimeSpan.Zero;
//	options.LoginPath = "Account/Login";
//});

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

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
