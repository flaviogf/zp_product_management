using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ZPProductManagement.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationStart(this IApplicationBuilder app, Func<ApplicationBuilderOptions, IServiceProvider> fn)
        {
            var options = new ApplicationBuilderOptions();

            var provider = fn(options);

            var context = provider.GetService<ApplicationDbContext>();

            var userManager = provider.GetService<UserManager<ApplicationUser>>();

            context.Database.Migrate();

            var userName = options.UserName;

            var email = options.Email;

            var password = options.Password;

            var userExists = userManager.FindByNameAsync(userName).Result;

            if (userExists != null)
            {
                return app;
            }

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email
            };

            userManager.CreateAsync(user, password).Wait();

            return app;
        }
    }

    public class ApplicationBuilderOptions
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
