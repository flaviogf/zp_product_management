using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ZPProductManagement.Application;
using ZPProductManagement.Application.Files;
using ZPProductManagement.Application.Products;
using ZPProductManagement.Web.Infrastructure;
using ZPProductManagement.Web.ViewModels;

namespace ZPProductManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>((it) => new SqlConnection(_configuration.GetConnectionString("Application")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, DapperCategoryRepository>();
            services.AddScoped<IFileRepository, DapperFileRepository>();
            services.AddScoped<IProductRepository, DapperProductRepository>();
            services.AddScoped<CreateFileApplication>();
            services.AddScoped<CreateProductApplication>();

            services.AddAutoMapper(it =>
            {
                it.CreateMap<IProductAdapter, IndexProductViewModel>();
            }, Assembly.GetExecutingAssembly());

            services.AddDbContext<ApplicationDbContext>(it => it.UseSqlite(_configuration.GetConnectionString("Identity")));

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.Configure<IdentityOptions>(it =>
            {
                it.Password.RequireDigit = false;
                it.Password.RequiredLength = 6;
                it.Password.RequiredUniqueChars = 1;
                it.Password.RequireLowercase = false;
                it.Password.RequireNonAlphanumeric = false;
                it.Password.RequireUppercase = false;

                it.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(it =>
            {
                it.Cookie.HttpOnly = true;
                it.LoginPath = "/SignIn";
                it.SlidingExpiration = true;
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(it => it.MapControllers());

            var userName = _configuration.GetValue<string>("User:UserName");

            var userExists = userManager.FindByNameAsync(userName).Result;

            if (userExists != null)
            {
                return;
            }

            var email = _configuration.GetValue<string>("User:Email");

            var password = _configuration.GetValue<string>("User:Password");

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email
            };

            userManager.CreateAsync(user, password).Wait();
        }
    }
}