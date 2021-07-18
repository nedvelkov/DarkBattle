namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.AreaCreatures;

    public class AreaCreaturesController : Controller
    {
        private readonly IAreaCreatureService service;
        private readonly IAreaService areaService;

        public AreaCreaturesController(IAreaCreatureService service,
                                        IAreaService areaService)
        {
            this.service = service;
            this.areaService = areaService;

        }

        public IActionResult Add(string creatureId, string areaId)
        {
            this.service.Add(creatureId, areaId);

            return Redirect($"/AreaCreatures/CreatureToArea?areaId={areaId}");
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

            return Redirect($"/AreaCreatures/CreaturesInArea?areaId={areaId}");
        }

    }
}
