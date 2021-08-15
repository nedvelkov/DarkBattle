namespace DarkBattle.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantItems;

    public class MerchantItemsController : AdminController
    {
        private readonly IMerchantItemsService service;
        private readonly IItemService itemService;
        private readonly IMerchantService merchantService;


        public MerchantItemsController(IMerchantItemsService service,
                                       IItemService itemService,
                                       IMerchantService merchantService)
        {
            this.service = service;
            this.itemService = itemService;
            this.merchantService = merchantService;
        }
        public IActionResult ListItems(string merchantId)
        {
            var model = new MerchantItemsToAddViewModel()
            {
                MerchantId = merchantId,
                MerchantName =this.merchantService.MerchantName(merchantId),
                ItemCollection = this.itemService.ItemsWithNoMerchant()
            };
            return View(model);
        }

        public IActionResult Add(string itemId, string merchantId)
        {
            this.service.Add(itemId, merchantId);
            return RedirectToAction("ListItems", "MerchantItems", new { merchantId = $"{merchantId}" });
        }
        public IActionResult Remove(string itemId, string merchantId)
        {
            this.service.Remove(itemId, merchantId);
            return RedirectToAction("ListItems", "MerchantItems", new { merchantId = $"{merchantId}" });
        }
        public IActionResult SellItems(MerchantItemPageModel model)
        {
            if (model.MerchantName == null)
            {
                model.MerchantName = this.merchantService.MerchantName(model.MerchantId);
                model.ItemCollection = this.itemService.ItemsSellByMerchant(model.MerchantId);
            }
            if (model.SelectItemLevel != 0)
            {
                model.ItemCollection = SortList((x => x.RequiredLevel == model.SelectItemLevel), model.ItemCollection);
            }
            if (model.SelectItemType != null)
            {
                model.ItemCollection = SortList((x => x.Type == model.SelectItemType), model.ItemCollection);
            }
            if (model.SelectObteinBy != null)
            {
                model.ItemCollection = SortList((x => x.ObtainBy == model.SelectObteinBy), model.ItemCollection);
            }

            return View(model);
        }


        private ICollection<T> SortList<T>(Func<T, bool> func, ICollection<T> collection)
             => collection.Where(func).ToList();
    }
}
