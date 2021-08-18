namespace DarkBattle.Areas.Admin.Controllers
{
    using DarkBattle.Infrastructure;
    using DarkBattle.Services.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static DarkBattle.Areas.Admin.AdminConstants;



    [Authorize(Roles = AdministratorRoleName)]
    public class HomeController : AdminController
    {
        private readonly IPlayerService playerService;

        public HomeController(IPlayerService playerService) 
            => this.playerService = playerService;

        public IActionResult Index()
            => View(this.playerService.AdminView());

        public IActionResult BanPlayer(string userId)
        {
            if (this.User.GetId() != userId)
            {
            this.playerService.BanPlayer(userId);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveBan(string userId)
        {
            this.playerService.RemoveBan(userId);
            return RedirectToAction("Index");
        }
    }
}
