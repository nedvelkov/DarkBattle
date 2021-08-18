namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using static DarkBattle.DarkBattleRoles;
    using DarkBattle.ViewModels.Merchants;
    using System.Linq;

    [Authorize(Roles = PlayerRoleName)]
    public class MerchantsController:Controller
    {
        private readonly IMerchantService merchantService;

        public MerchantsController(IMerchantService merchantService) => this.merchantService = merchantService;

        public IActionResult Index()
        {

            return View(this.merchantService.MerchantsCollection());
        }

        public IActionResult Market(string championId,string page)
        {
            if (championId == null)
            {
                return RedirectToAction("Index", "Champions");
            }

            var playerId = this.User.GetId();
            var model = this.merchantService.AllMerchants(championId, playerId);
            var returnModel = new MarketViewModel
            {
                Champion = model.Champion,
                Merchants = model.Merchants.OrderBy(x=>x.Name).ToList(),
                MaxPages = model.Merchants.Count
            };
            
            if (page != null)
            {
                returnModel.CurrentPage = int.Parse(page);
            }

            returnModel.Merchants = returnModel.Merchants.Skip((returnModel.CurrentPage - 1)*returnModel.MaxMerchantsPerPage).Take(returnModel.MaxMerchantsPerPage).ToList();

            return View(returnModel);
        }
    }
}
