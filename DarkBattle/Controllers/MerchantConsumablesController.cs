namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantConsumables;
    using DarkBattle.Infrastructure;
    using DarkBattle.Services.ServiceModels;

    public class MerchantConsumablesController:Controller
    {
        private readonly IMerchantConsumablesService service;
        private readonly IChampionService championService;

        public MerchantConsumablesController(IMerchantConsumablesService service, IChampionService championService)
        {
            this.service = service;
            this.championService = championService;
        }

        public IActionResult ListConsumables(string merchantId)
            => View(this.service.Consumables(merchantId));

        public IActionResult Add(string consumableId, string merchantId)
        {
            this.service.Add(consumableId, merchantId);
            return Redirect($"/MerchantConsumables/ListConsumables?merchantId={merchantId}");
        }
        public IActionResult Remove(string itemId, string merchantId)
        {
            this.service.Remove(itemId, merchantId);
            return Redirect($"/MerchantConsumables/ListConsumables?merchantId={merchantId}");
        }
        public IActionResult SellConsumables(MerchantConsumablePageModel model)
        {
            var merchant = this.service.SortedConsumablesSellByMerchant(model);
            return View(merchant);
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

            if (result == false)
            {
                return RedirectToAction("SellConsumablesToChampion", "MerchantConsumables", new { championId = $"{championId}", merchantId = $"{merchantId}" ,error="Already have this consumable" });

            }
            return RedirectToAction("SellConsumablesToChampion", "MerchantConsumables", new { championId = $"{championId}", merchantId = $"{merchantId}" });
        }
    }
}
