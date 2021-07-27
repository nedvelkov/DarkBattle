﻿namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Merchants;

    public class MerchantsController:Controller
    {
        private readonly IMerchantService merchantService;

        public MerchantsController(IMerchantService merchantService) => this.merchantService = merchantService;

        public IActionResult Index()
        {

            return View(this.merchantService.MerchantsCollection());
        }

        public IActionResult Create([FromQuery] string merchantId)
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
        public IActionResult Create(MerchantViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.merchantService.Add(model);

            return Redirect("/Merchants");
        }

        public IActionResult Edit([FromQuery] string merchantId)
        {
            var creature = this.merchantService.GetMerchant(merchantId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(MerchantViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.merchantService.Edit(model);

            return Redirect("/Merchants");
        }

        public IActionResult Delete([FromQuery] string merchantId)
        {
            if (this.merchantService.Delete(merchantId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Merchants");
        }
    }
}