namespace DarkBattle.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantConsumables;
    using DarkBattle.ViewModels.Enums;

    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
    public class MerchantConsumablesController:AdminController
    {
        private readonly IMerchantConsumablesService service;
        private readonly IConsumableService consumableService;
        private readonly IMerchantService merchantService;


        public MerchantConsumablesController(IMerchantConsumablesService service,
                                             IConsumableService consumableService,
                                             IMerchantService merchantService)
        {
            this.service = service;
            this.consumableService = consumableService;
            this.merchantService = merchantService;
        }

        public IActionResult ListConsumables(string merchantId)
        {
            var model = new MerchantConsumablesAddViewModel
            {
                MerchantId = merchantId,
                MerchantName = this.merchantService.MerchantName(merchantId),
                Consumables = this.consumableService.ConsumablesWithNoMerchant()
            };
            return View(model);
        }

        public IActionResult Add(string consumableId, string merchantId)
        {
            this.service.Add(consumableId, merchantId);
            return RedirectToAction("ListConsumables", "MerchantConsumables", new { merchantId = $"{merchantId}" });
        }
        public IActionResult Remove(string itemId, string merchantId)
        {
            this.service.Remove(itemId, merchantId);
            return RedirectToAction("ListConsumables", "MerchantConsumables", new { merchantId = $"{merchantId}" });

        }
        public IActionResult SellConsumables(MerchantConsumablePageModel model)
        {
            if (model.MerchantName == null)
            {
                model.MerchantName = this.merchantService.MerchantName(model.MerchantId);
                var tmp= this.consumableService.ConsumablesSellByMerchant(model.MerchantId);
                model.Consumables = tmp;
            }
            if (model.SearchByName != null)
            {
                model.Consumables= SortList(x => x.Name.ToLower().Contains(model.SearchByName.ToLower()), model.Consumables);
            }
            model.Consumables = model.Sorting switch
            {
                ConsumableSorting.Value => model.Consumables.OrderByDescending(x => x.Value).ToList(),
                ConsumableSorting.Healt or _ => model.Consumables.OrderByDescending(x => x.RestoreHealth).ToList(),
            };

            return View(model);
        }
        private ICollection<T> SortList<T>(Func<T, bool> func, ICollection<T> collection)
             => collection.Where(func).ToList();
    }
}
