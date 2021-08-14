namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.AreaCreatures;

    public class AreaCreaturesController : AdminController
    {
        private readonly IAreaCreatureService service;

        public AreaCreaturesController(IAreaCreatureService service) => this.service = service;


        public IActionResult Add(string creatureId, string areaId)
        {
            this.service.Add(creatureId, areaId);

            return RedirectToAction("CreatureToArea", "AreaCreatures", new { areaId = $"{areaId}" });
        }

        public IActionResult CreatureToArea(string areaId)
        {
            var creatures = this.service.ListAllAvilableCreatures(areaId);
            return View(creatures);
        }

        public IActionResult CreaturesInArea(string areaId, AreaCreaturesPageModel model)
        {
            AreaCreaturesPageModel creatures;
            if (areaId != null)
            {
                creatures = this.service.ListAllCreaturesInArea(areaId);
            }
            else
            {
                creatures = this.service.SortCreatures(model);
            }

            return View(creatures);
        }

        public IActionResult Remove(string creatureId, string areaId)
        {
            this.service.Remove(creatureId, areaId);
            return RedirectToAction("CreatureToArea", "AreaCreatures", new { areaId = $"{areaId}" });
        }

    }
}
