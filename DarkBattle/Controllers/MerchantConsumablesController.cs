namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using static DarkBattle.DarkBattleRoles;
    using DarkBattle.ViewModels.Merchants;
    using DarkBattle.ViewModels.MerchantConsumables;

    [Authorize(Roles = PlayerRoleName)]
    public class MerchantConsumablesController:Controller
    {
        private readonly IMerchantConsumablesService service;
        private readonly IChampionService championService;
        private readonly IConsumableService consumableService;
        private readonly IMerchantService merchantService;

        public MerchantConsumablesController(IMerchantConsumablesService service,
                                             IChampionService championService,
                                             IConsumableService consumableService,
                                             IMerchantService merchantService)
        {
            this.service = service;
            this.championService = championService;
            this.consumableService = consumableService;
            this.merchantService = merchantService;
        }
        public IActionResult SellConsumablesToChampion(string championId, string merchantId)
        {
            var playerId = this.User.GetId();
            var champion = this.championService.ChampionBar(championId, playerId);
            var consumables = this.consumableService.ConsumablesSellByMerchant(merchantId);
            var model = new SellConsumablesViewModel
            {
                Champion = champion,
                Consumables = new MerchantConsumablePageModel
                {
                    MerchantId=merchantId,
                    MerchantName=this.merchantService.MerchantName(merchantId),
                    Consumables = consumables
                }
            };
            return View(model);
        }

        public IActionResult SellConsumable(string championId, string consumableId, string merchantId)
        {
            var result= this.service.SellConsumable(championId, consumableId);
            return RedirectToAction("SellConsumablesToChampion", "MerchantConsumables", new { championId = $"{championId}", merchantId = $"{merchantId}" });
        }
    }
}
