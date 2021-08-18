namespace DarkBattle.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using DarkBattle.ViewModels.Merchants;
    using DarkBattle.ViewModels.MerchantItems;
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Items;

    using static DarkBattle.DarkBattleRoles;
    
    [Authorize(Roles = PlayerRoleName)]
    public class MerchantItemsController : Controller
    {
        private readonly IMerchantItemsService service;
        private readonly IChampionService championService;
        private readonly IMerchantService merchantService;
        private readonly IItemService itemService;

        public MerchantItemsController(IMerchantItemsService service,
                                       IChampionService championService,
                                       IItemService itemService,
                                       IMerchantService merchantService)
        {
            this.service = service;
            this.championService = championService;
            this.itemService = itemService;
            this.merchantService = merchantService;
        }

        public IActionResult SellItemsToChampion(string championId, string merchantId, string page, string sort)
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
                    MerchantName = this.merchantService.MerchantName(merchantId),
                    ItemCollection = items,
                },
                MaxPages = items.Count,
            };
            if (page != null)
            {
                model.CurrentPage = int.Parse(page);
            }
            model.Items.ItemCollection = model.Items.ItemCollection.Skip((model.CurrentPage - 1)*model.MaxItemsPerPage).Take(model.MaxItemsPerPage).ToList();
            if (sort != null)
            {
            model.Items.ItemCollection = SortCollection(sort, model.Items.ItemCollection);
            }
            return View(model);
        }

        public IActionResult SellItem(string championId, string itemId, string merchantId)
        {
            this.service.SellItem(championId, itemId);
            return RedirectToAction("SellItemsToChampion", "MerchantItems", new { championId = $"{ championId}", merchantId = $"{merchantId}" });

        }

        private ICollection<ItemServiceModel> SortCollection(string sort, ICollection<ItemServiceModel> collection)
        {
            collection = sort.ToLower() switch
            {
                "obtainby" => collection.OrderByDescending(x => x.ObtainBy).ToList(),
                "type" => collection.OrderByDescending(x => x.Type).ToList(),
                "requiredlevel" => collection.OrderByDescending(x => x.RequiredLevel).ToList(),
                "value" => collection.OrderByDescending(x => x.Value).ToList(),
                "name" or _ => collection.OrderByDescending(x => x.Name).ToList(),
            };
            return collection;
        }
    }
}
