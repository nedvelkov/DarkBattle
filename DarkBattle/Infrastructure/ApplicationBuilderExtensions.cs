namespace DarkBattle.Infrastructure
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    
    using DarkBattle.Data;
    using DarkBattle.Data.Models;

    using static DarkBattle.DarkBattleRoles;
    using DarkBattle.Seeder;

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

            SeedAdministrator(services);
            SeedPlayerRole(services);
            var seeder = new SeedData(data);
            seeder.Seed();

            return app;
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<Player>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@darkbattle.com";
                    const string adminPassword = "admin123";

                    var user = new Player
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
        private static void SeedPlayerRole(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(PlayerRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = PlayerRoleName };

                    await roleManager.CreateAsync(role);

                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
