using Company.Data.Contexts;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interface;
using Company.Service.Services;
using Company.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using Company.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Company.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(config => {
                config.Password.RequiredUniqueChars = 2; 
                config.Password.RequireDigit = true; 
                config.Password.RequireLowercase = true; 
                config.Password.RequireUppercase = true; 
                config.User.RequireUniqueEmail = true; 

            }).AddEntityFrameworkStores<CompanyDbContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(op => { 
                op.Cookie.HttpOnly = true;  
                op.Cookie.Name = "SecurityCookie";
                op.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                op.Cookie.SameSite = SameSiteMode.Strict;
                op.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                op.SlidingExpiration = true;
                op.LoginPath = "/Account/Login";
                op.LogoutPath = "/Account/Logout";
                op.AccessDeniedPath = "/Account/AccessDeny";
            });
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new CustomAutoMapper()));

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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
