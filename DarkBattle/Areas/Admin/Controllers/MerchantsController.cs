namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Merchants;

    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
    public class MerchantsController:AdminController
    {
        private readonly IMerchantService merchantService;

        public MerchantsController(IMerchantService merchantService) 
            => this.merchantService = merchantService;

        public IActionResult Index()
        {

            return View(this.merchantService.MerchantsCollection());
        }

        public IActionResult Create(string merchantId)
        {
            if (merchantId != null)
            {
                var copyCreature = this.merchantService
                                       .GetMerchant(merchantId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(MerchantServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.merchantService.Add(model);

            return RedirectToAction("Index");

        }

        public IActionResult Edit(string merchantId)
        {
            var creature = this.merchantService.GetMerchant(merchantId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(MerchantServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.merchantService.Edit(model);

            return RedirectToAction("Index");

        }

        public IActionResult Delete( string merchantId)
        {
            if (this.merchantService.Delete(merchantId) == false)
            {
                return Redirect("/Home/Error");
            }
            return RedirectToAction("Index");

        }
    }
}
