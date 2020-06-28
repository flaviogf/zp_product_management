using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ZPProductManagement.Api.Infrastructure;
using ZPProductManagement.Api.Repositories;
using ZPProductManagement.Api.ViewModels;
using ZPProductManagement.Application;

namespace ZPProductManagement.Api
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
            services.AddScoped<IDbConnection>(it => new SqlConnection(_configuration.GetConnectionString("Default")));

            services.AddAutoMapper((it) =>
            {
                it.CreateMap<CreatedFile, ShowFileViewModel>();
                it.CreateMap<StoredFile, ShowFileViewModel>();
            }, Assembly.GetExecutingAssembly());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFileRepository, DapperFileRepository>();
            services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddScoped<CreateFileApplication>();

            services.AddControllers();
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
