namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;
    using DarkBattle.Services.ServiceModels;

    using static DarkBattle.DarkBattleRoles;

    [Authorize(Roles = PlayerRoleName)]
    public class MerchantConsumablesController:Controller
    {
        private readonly IMerchantConsumablesService service;
        private readonly IChampionService championService;

        public MerchantConsumablesController(IMerchantConsumablesService service, IChampionService championService)
        {
            this.service = service;
            this.championService = championService;
        }
        public IActionResult SellConsumablesToChampion(string championId, string merchantId)
        {
            var playerId = this.User.GetId();
            var champion = this.championService.ChampionBar(championId, playerId);
            var consumables = this.service.ConsumablesSellByMerchant(merchantId);
            var model = new SellConsumablesViewModel
            {
                Champion = champion,
                Consumables = consumables
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
