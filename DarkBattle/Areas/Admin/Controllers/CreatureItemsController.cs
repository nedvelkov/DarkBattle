namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.CreatureItems;

    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
    public class CreatureItemsController : AdminController
    {
        private readonly ICreatureItemsService creatureItemsService;
        private readonly IItemService itemService;
        private readonly ICreatureService creatureService;

        public CreatureItemsController(ICreatureItemsService creatureItemsService,
                                       IItemService itemService,
                                       ICreatureService creatureService)
        {
            this.creatureItemsService = creatureItemsService;
            this.itemService = itemService;
            this.creatureService = creatureService;
        }

        public IActionResult CreatureItems(string creatureId)
        {
            var model = new CreateureItemViewModel
            {
                CreatureId = creatureId,
                CreatureName =this.creatureService.CreatureName(creatureId),
                ItemCollection = this.itemService.CreatureItems(creatureId)
            };
            return View(model);
        }

        public IActionResult ListItemsToAdd(string creatureId)
        {
            var model = new CreateureItemViewModel
            {
                CreatureId = creatureId,
                CreatureName = this.creatureService.CreatureName(creatureId),
                ItemCollection = this.itemService.ItemsWithNoCreature()
            };
            return View(model);
        }

        public IActionResult Add(string itemId, string creatureId)
        {
           var result= this.creatureItemsService.Add(itemId, creatureId);
            return RedirectToAction("ListItemsToAdd", "CreatureItems", new { creatureId = $"{creatureId}" });
        }
        public IActionResult Remove(string itemId, string creatureId)
        {
            var reseult= this.creatureItemsService.Remove(itemId, creatureId);

            return RedirectToAction("CreatureItems", "CreatureItems", new { creatureId = $"{creatureId}" });
        }
    }
}
