namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantConsumables;


    public class MerchantConsumablesController:AdminController
    {
        private readonly IMerchantConsumablesService service;


        public MerchantConsumablesController(IMerchantConsumablesService service) 
            => this.service = service;

        public IActionResult ListConsumables(string merchantId)
            => View(this.service.Consumables(merchantId));

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
            var merchant = this.service.SortedConsumablesSellByMerchant(model);
            return View(merchant);
        }
    }
}
