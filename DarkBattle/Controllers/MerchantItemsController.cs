namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantItems;
    using DarkBattle.Infrastructure;
    using DarkBattle.Services.ServiceModels;

    public class MerchantItemsController:Controller
    {
        private readonly IMerchantItemsService service;
        private readonly IChampionService championService;

        public MerchantItemsController(IMerchantItemsService service, IChampionService championService)
        {
            this.service = service;
            this.championService = championService;
        }
        public IActionResult ListItems(string merchantId)
            => View(this.service.Items(merchantId));

        public IActionResult Add(string itemId, string merchantId)
        {
            this.service.Add(itemId, merchantId);
            return Redirect($"/MerchantItems/ListItems?merchantId={merchantId}");
        }
        public IActionResult Remove(string itemId, string merchantId)
        {
            this.service.Remove(itemId, merchantId);
            return Redirect($"/MerchantItems/ListItems?merchantId={merchantId}");
        }
        public IActionResult SellItems(MerchantItemPageModel model)
        {
             var  merchant = this.service.SortedItemsSellByMerchant(model);
            return View(merchant);
        }

        public IActionResult SellItemsToChampion(string championId,string merchantId)
        {
            var playerId = this.User.GetId();
            var champion = this.championService.ChampionBar(championId, playerId);
            var items = this.service.ItemsSellByMerchant(merchantId);
            var model = new SellItemsViewModel
            {
                Champion = champion,
                Items = items
            };
            return View(model);
        }
    }
}
