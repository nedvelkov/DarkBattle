namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using static DarkBattle.DarkBattleRoles;
    using DarkBattle.ViewModels.Merchants;
    using DarkBattle.ViewModels.MerchantItems;

    [Authorize(Roles = PlayerRoleName)]
    public class MerchantItemsController : Controller
    {
        private readonly IMerchantItemsService service;
        private readonly IChampionService championService;
        private readonly IMerchantService merchantService;
        private readonly IItemService itemService;

        public MerchantItemsController(IMerchantItemsService service, IChampionService championService)
        {
            this.service = service;
            this.championService = championService;
        }

        public IActionResult SellItemsToChampion(string championId, string merchantId)
        {
            var playerId = this.User.GetId();
            var champion = this.championService.ChampionBar(championId, playerId);
            var items = this.itemService.ItemsSellByMerchant(merchantId);
            var model = new SellItemsViewModel
            {
                Champion = champion,
                Items = new MerchantItemPageModel
                {
                    MerchantId = merchantId,
                    MerchantName=this.merchantService.MerchantName(merchantId),
                    ItemCollection=items
                }
            };
            return View(model);
        }

        public IActionResult SellItem(string championId, string itemId, string merchantId)
        {
            this.service.SellItem(championId, itemId);
            return RedirectToAction("SellItemsToChampion", "MerchantItems", new { championId = $"{ championId}", merchantId = $"{merchantId}" });

        }
    }
}
