namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.MerchantItems;


    public class MerchantItemsController : AdminController
    {
        private readonly IMerchantItemsService service;


        public MerchantItemsController(IMerchantItemsService service) 
            => this.service = service;
        public IActionResult ListItems(string merchantId)
            => View(this.service.Items(merchantId));

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
            var merchant = this.service.SortedItemsSellByMerchant(model);
            return View(merchant);
        }
    }
}
