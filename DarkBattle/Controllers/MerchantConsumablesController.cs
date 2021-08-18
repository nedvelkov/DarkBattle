namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.Services.Interface;
    using DarkBattle.Infrastructure;

    using static DarkBattle.DarkBattleRoles;
    using DarkBattle.ViewModels.Merchants;
    using DarkBattle.ViewModels.MerchantConsumables;
    using System.Linq;
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Consumables;

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
        public IActionResult SellConsumablesToChampion(string championId, string merchantId, string page, string sort)
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
                },
                MaxPages=consumables.Count,   
            };

            if (page != null)
            {
                model.CurrentPage = int.Parse(page);
            }
            model.Consumables.Consumables = model.Consumables.Consumables.Skip((model.CurrentPage - 1)*model.MaxConsumablesPerPage).Take(model.MaxConsumablesPerPage).ToList();
            if (sort != null)
            {
                model.Consumables.Consumables = SortCollection(sort, model.Consumables.Consumables);
            }

            return View(model);
        }

        public IActionResult SellConsumable(string championId, string consumableId, string merchantId)
        {
            var result= this.service.SellConsumable(championId, consumableId);
            return RedirectToAction("SellConsumablesToChampion", "MerchantConsumables", new { championId = $"{championId}", merchantId = $"{merchantId}" });
        }

        private ICollection<ConsumableViewServiceModel> SortCollection(string sort, ICollection<ConsumableViewServiceModel> collection)
        {
            collection = sort.ToLower() switch
            {
                "restorehealth" => collection.OrderByDescending(x => x.RestoreHealth).ToList(),
                "value" => collection.OrderByDescending(x => x.Value).ToList(),
                "name" or _ => collection.OrderByDescending(x => x.Name).ToList(),
            };
            return collection;
        }
    }
}
