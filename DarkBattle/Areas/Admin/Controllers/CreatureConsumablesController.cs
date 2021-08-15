namespace DarkBattle.Areas.Admin.Controllers
{
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.CreatureConsumables;
    using Microsoft.AspNetCore.Mvc;

    public class CreatureConsumablesController:AdminController
    {
        private readonly ICreatureConsumablesService creatureConsumablesService;
        private readonly ICreatureService creatureService;
        private readonly IConsumableService consumableService;

        public CreatureConsumablesController(ICreatureConsumablesService creatureConsumablesService,
                                             IConsumableService consumableService,
                                             ICreatureService creatureService)
        {
            this.creatureConsumablesService = creatureConsumablesService;
            this.consumableService = consumableService;
            this.creatureService = creatureService;
        }

        public IActionResult CreatureConsumables(string creatureId)
        {
            var model = new CreatureConsumableViewModel
            {
                CreatureId = creatureId,
                CreatureName = this.creatureService.CreatureName(creatureId),
                Consumables = this.consumableService.ConsumablesOwnByCreature(creatureId)
            };

            return View(model);
        }

        public IActionResult ListConsumablesToAdd(string creatureId)
        {
            var model = new CreatureConsumableViewModel
            {
                CreatureId = creatureId,
                CreatureName = this.creatureService.CreatureName(creatureId),
                Consumables = this.consumableService.ConsumablesWithNoCreature()
            };

            return View(model);
        }

        public IActionResult Add(string consumableId, string creatureId)
        {
            var result = this.creatureConsumablesService.Add(consumableId, creatureId);
            return RedirectToAction("ListConsumablesToAdd", "CreatureConsumables", new { creatureId = $"{creatureId}" });
        }
        public IActionResult Remove(string consumableId, string creatureId)
        {
            var result = this.creatureConsumablesService.Remove(consumableId, creatureId);
            return RedirectToAction("CreatureConsumables", "CreatureConsumables", new { creatureId = $"{creatureId}" });
        }
    }
}
