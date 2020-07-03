using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using ZPProductManagement.Application;
using ZPProductManagement.Web.Infrastructure;

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
            services.AddScoped<IDbConnection>((it) => new SqlConnection(_configuration.GetConnectionString("Default")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileRepository, DapperFileRepository>();
            services.AddScoped<CreateFileApplication>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(it => it.MapControllers());
        }
    }
}
