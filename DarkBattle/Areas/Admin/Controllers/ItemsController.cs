namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Items;
    using DarkBattle.ViewModels.Items;

    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
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
        public IActionResult Create(ItemViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            var returnModel = new ItemServiceModel
            {
                Name = model.Name,
                Attack = model.Attack,
                Defense = model.Defense,
                ObtainBy = model.ObtainBy,
                Type = model.Type,
                RequiredLevel = model.RequiredLevel,
                ImageUrl = model.ImageUrl,
                Value = model.Value
            };

            this.itemService.Add(returnModel);

            return RedirectToAction("Index");

        }

        public IActionResult Edit( string itemId)
        {
            var creature = this.itemService.GetItem(itemId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }
            var championClasses = this.championClassService.ChampionClassCollection();

            var returnModel = new ItemViewModel
            {
                Id=creature.Id,
                Name = creature.Name,
                Attack = creature.Attack,
                Defense = creature.Defense,
                ObtainBy = creature.ObtainBy,
                Type = creature.Type,
                RequiredLevel = creature.RequiredLevel,
                ImageUrl = creature.ImageUrl,
                Value = creature.Value,
                ChampionClasses=championClasses
            };

            return View(returnModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            var returnModel = new ItemServiceModel
            {
                Id=model.Id,
                Name = model.Name,
                Attack = model.Attack,
                Defense = model.Defense,
                ObtainBy = model.ObtainBy,
                Type = model.Type,
                RequiredLevel = model.RequiredLevel,
                ImageUrl = model.ImageUrl,
                Value = model.Value
            };

            this.itemService.Edit(returnModel);

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
