namespace DarkBattle.Controllers
{
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Items;
    using Microsoft.AspNetCore.Mvc;
    public class ItemsController:Controller
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService) => this.itemService = itemService;

        public IActionResult Index()
        {

            return View(this.itemService.ItemsCollection());
        }

        public IActionResult Create(string itemId)
        {
            if (itemId != null)
            {
                var copyCreature = this.itemService
                                       .GetItem(itemId);
                return View(copyCreature);
            }

            return View(this.itemService.GetChamponClasses());
        }

        [HttpPost]
        public IActionResult Create(ItemViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.itemService.Add(model);

            return Redirect("/Items");
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

            this.itemService.Edit(model);

            return Redirect("/Items");
        }

        public IActionResult Delete([FromQuery] string itemId)
        {
            if (this.itemService.Delete(itemId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Items");

        }
    }
}
