namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Items;
    using DarkBattle.ViewModels.Items;

    public class ItemsController:AdminController
    {
        private readonly IItemService itemService;
        private readonly IChampionClassService championClassService;

        public ItemsController(IItemService itemService, IChampionClassService championClassService)
        {
            this.itemService = itemService;
            this.championClassService = championClassService;
        }

        public IActionResult Index()
        {
            return View(this.itemService.ItemsCollection());
        }

        public IActionResult Create(string itemId)
        {
            var championClasses = this.championClassService.ChampionClassCollection();
            if (itemId != null)
            {
                var copyCreature = this.itemService
                                       .GetItem(itemId);
                var returnCopy = new ItemViewModel
                {
                    Name = copyCreature.Name,
                    Attack = copyCreature.Attack,
                    Defense = copyCreature.Defense,
                    ImageUrl = copyCreature.ImageUrl,
                    Type = copyCreature.Type,
                    ObtainBy = copyCreature.ObtainBy,
                    Value = copyCreature.Value,
                    RequiredLevel = copyCreature.RequiredLevel,
                    ChampionClasses=championClasses
                };
                return View(returnCopy);
            }

            var model = new ItemViewModel
            {
                ChampionClasses = championClasses
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ItemServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.itemService.Add(model);

            return RedirectToAction("Index");

        }

        public IActionResult Edit( string itemId)
        {
            var creature = this.itemService.GetItem(itemId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(ItemServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.itemService.Edit(model);

            return RedirectToAction("Index");

        }

        public IActionResult Delete(string itemId)
        {
            if (this.itemService.Delete(itemId) == false)
            {
                return Redirect("/Home/Error");
            }
            return RedirectToAction("Index");

        }
    }
}
