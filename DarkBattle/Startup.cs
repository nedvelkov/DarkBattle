namespace DarkBattle
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using DarkBattle.Data;
    using DarkBattle.MappingConfiguration;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.Models;
    using DarkBattle.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using DarkBattle.Data.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<Player>(options => {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews(config=>
            {
                config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddAutoMapper(typeof(DarkBattleProfile))
                    .AddTransient<ICreatureService, CreatureService>()
                    .AddTransient<IAreaService, AreaService>()
                    .AddTransient<IValidator, Validator>()
                    .AddTransient<IItemService, ItemService>()
                    .AddTransient<IConsumableService, ConsumableService>()
                    .AddTransient<IMerchantService, MerchantService>()
                    .AddTransient<IChampionClassService, ChampionClassService>()
                    .AddTransient<IAreaCreatureService, AreaCreatureService>()
                    .AddTransient<IMerchantItemsService, MerchantItemsService>()
                    .AddTransient<IMerchantConsumablesService, MerchantConsumablesService>()
                    .AddTransient<IStatisticService, StatisticService>()
                    .AddTransient<IPlayerService, PlayerService>()
                    .AddTransient<IChampionService, ChampionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseAuthentication()
                .UseEndpoints
                    (endpoints =>
                        {
                            endpoints.MapDefaultControllerRoute();
                            endpoints.MapRazorPages();
                        }
                    );
        }
    }
}
