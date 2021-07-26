namespace DarkBattle.Infrastructure
{
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    
    using DarkBattle.Data;

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            var roles= data.UserRoles.Any();

            if (roles == false)
            {
                //data.UserRoles.Add()
            }
          //  SeedCategories(data);

            return app;
        }
    }
}
