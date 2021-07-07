﻿namespace DarkBattle.Controllers
{
    using DarkBattle.Services;
    using DarkBattle.ViewModels.Items;
    using Microsoft.AspNetCore.Mvc;
    public class ItemController:Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService) => this.itemService = itemService;

        public IActionResult Index()
        {

            return View(this.itemService.ItemCollection());
        }

        public IActionResult Create([FromQuery] string itemId)
        {
            if (itemId != null)
            {
                var copyCreature = this.itemService
                                       .GetItem(itemId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.itemService.AddItem(model);

            return Redirect("/Item");
        }

        public IActionResult Edit([FromQuery] string itemId)
        {
            var creature = this.itemService.GetItem(itemId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(ItemViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.itemService.EditItem(model);

            return Redirect("/Item");
        }

        public IActionResult Delete([FromQuery] string itemId)
        {
            if (this.itemService.DeleteItem(itemId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Item");

        }
    }
}