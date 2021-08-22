namespace DarkBattle.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.AreaCreatures;
    using DarkBattle.ViewModels.Enums;


    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
    public class AreaCreaturesController : AdminController
    {
        private readonly IAreaCreatureService service;
        private readonly ICreatureService creatureService;
        private readonly IAreaService areaService;

        public AreaCreaturesController(IAreaCreatureService service,
                                       ICreatureService creatureService,
                                       IAreaService areaService)
        {
            this.service = service;
            this.creatureService = creatureService;
            this.areaService = areaService;
        }


        public IActionResult Add(string creatureId, string areaId)
        {
            this.service.Add(creatureId, areaId);

            return RedirectToAction("CreatureToArea", "AreaCreatures", new { areaId = $"{areaId}" });
        }

        public IActionResult CreatureToArea(string areaId)
        {
            var area = this.areaService.AreaForCreatures(areaId);
            var model = new AreaCreatureAddViewModel
            {
                Area = area,
                Creatures = this.creatureService.CreatureWithNoArea(area.MinLevel, area.MaxLevel)
            };

            return View(model);
        }

        public IActionResult CreaturesInArea(string areaId, AreaCreaturesPageModel model)
        {
            var area = this.areaService.AreaForCreatures(areaId == null ? model.Area.Id : areaId);

            var listCreatures = this.creatureService.CreatureInArea(areaId == null ? model.Area.Id : areaId);

            if (model.SearchTerm != null)
            {
                listCreatures = FilterList(x => x.Name.ToLower().Contains(model.SearchTerm.ToLower()), listCreatures);
            }

            listCreatures = model.Sorting switch
            {
                CreatureSorting.Attack => listCreatures.OrderByDescending(x => x.Attack).ToList(),
                CreatureSorting.Expirience => listCreatures.OrderByDescending(x => x.Expirience).ToList(),
                CreatureSorting.Name => listCreatures.OrderByDescending(x => x.Name).ToList(),
                CreatureSorting.Level or _ => listCreatures.OrderByDescending(x => x.Level).ToList(),
            };

            if (model.CurrentLevel != null)
            {
                listCreatures = FilterList(x => x.Level == model.CurrentLevel, listCreatures);
            }

            model.Area = area;
            model.Creatures = listCreatures;

            return View(model);
        }

        public IActionResult Remove(string creatureId, string areaId)
        {
            this.service.Remove(creatureId, areaId);
            return RedirectToAction("CreatureToArea", "AreaCreatures", new { areaId = $"{areaId}" });
        }

        private ICollection<T> FilterList<T>(Func<T, bool> func, ICollection<T> collection)
                 => collection.Where(func).ToList();
    }
}
