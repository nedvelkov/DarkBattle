namespace DarkBattle.Areas.Admin.Controllers
{
    using DarkBattle.Services.Interface;
    using Microsoft.AspNetCore.Mvc;

    public class CreatureConsumablesController:AdminController
    {
        private readonly ICreatureConsumablesService creatureConsumablesService;

        public CreatureConsumablesController(ICreatureConsumablesService creatureConsumablesService)
        {
            this.creatureConsumablesService = creatureConsumablesService;
        }

        public IActionResult CreatureConsumables(string creatureId)
            => View(this.creatureConsumablesService.Items(creatureId));

        public IActionResult ListConsumablesToAdd(string creatureId)
            => View(this.creatureConsumablesService.AddItems(creatureId));

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
