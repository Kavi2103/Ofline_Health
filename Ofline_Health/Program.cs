using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ofline_Health.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Ofline_HealthContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Ofline_HealthContext") ?? throw new InvalidOperationException("Connection string 'Ofline_HealthContext' not found.")));

builder.Services.AddControllersWithViews();

// Add session and cookie authentication services
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Ofline_Health.Session";
    options.IdleTimeout = System.TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
      options.LoginPath = "/Admin/Login";
      options.AccessDeniedPath = "/Admin/Login";



      options.LoginPath = "/Reception/Login";
      options.AccessDeniedPath = "/Reception/Login";


      options.LoginPath = "/Doctor/Login";
      options.AccessDeniedPath = "/Doctor/Login";
  });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session and authentication middleware
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Doctor}/{action=Login}/{id?}");

app.Run();

