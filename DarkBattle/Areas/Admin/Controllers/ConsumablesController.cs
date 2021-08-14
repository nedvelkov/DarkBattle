namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Consumables;

    public class ConsumablesController : AdminController
    {
        private readonly IConsumableService consumableService;

        public ConsumablesController(IConsumableService consumableService) => this.consumableService = consumableService;

        public IActionResult Index()
        {

            return View(this.consumableService.ConsumablesCollection());
        }

        public IActionResult Create(string consumableId)
        {
            if (consumableId != null)
            {
                var copyCreature = this.consumableService
                                       .GetConsumable(consumableId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(ConsumableViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.consumableService.Add(model);

            return Redirect("/Consumables");
        }

        public IActionResult Edit(string consumableId)
        {
            var creature = this.consumableService.GetConsumable(consumableId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(ConsumableViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.consumableService.Edit(model);

            return Redirect("/Consumables");
        }

        public IActionResult Delete(string consumableId)
        {
            if (this.consumableService.Delete(consumableId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Consumables");

        }
    }
}
