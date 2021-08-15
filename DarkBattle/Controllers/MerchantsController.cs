namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using static DarkBattle.DarkBattleRoles;
  

    [Authorize(Roles = PlayerRoleName)]
    public class MerchantsController:Controller
    {
        private readonly IMerchantService merchantService;

        public MerchantsController(IMerchantService merchantService) => this.merchantService = merchantService;

        public IActionResult Index()
        {

            return View(this.merchantService.MerchantsCollection());
        }

        public IActionResult Market(string championId)
        {
            if (championId == null)
            {
                return RedirectToAction("Index", "Champions");
            }

            var playerId = this.User.GetId();
            var model = this.merchantService.AllMerchants(championId, playerId);
            return View(model);
        }
    }
}
