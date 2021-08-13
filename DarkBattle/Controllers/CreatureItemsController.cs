namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;


    public class CreatureItemsController : Controller
    {
        private readonly ICreatureItemsService creatureItemsService;

        public CreatureItemsController(ICreatureItemsService creatureItemsService)
        {
            this.creatureItemsService = creatureItemsService;
        }

        public IActionResult CreatureItems(string creatureId)
            => View(this.creatureItemsService.Items(creatureId));

        public IActionResult ListItemsToAdd(string creatureId)
            => View(this.creatureItemsService.AddItems(creatureId));

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
