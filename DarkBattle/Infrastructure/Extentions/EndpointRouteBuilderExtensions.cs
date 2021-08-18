namespace DarkBattle.Infrastructure.Extentions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                                name: "Admin",
                                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        public static void MapDefaultRoute(this IEndpointRouteBuilder endpoints)
                => endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");

    }
}
