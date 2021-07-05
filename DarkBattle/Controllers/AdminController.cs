namespace DarkBattle.Controllers
{
    using DarkBattle.Data;
    using DarkBattle.Models;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Creatures;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DarkBattle.Services;

    public class AdminController : Controller
    {
        private readonly ICreatureService creatureService;
        private readonly IAreaService areaService;

        public AdminController(ICreatureService creatureService,
                                IAreaService areaService)
        {
            this.creatureService = creatureService;
            this.areaService = areaService;
        }

        public IActionResult AddCreature([FromQuery] string creatureId)
        {

            if (creatureId != null)
            {
                var copyCreature = this.creatureService
                                       .ReturnCreatureViewModel(creatureId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddCreature(CreateCreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.creatureService.AddCreature(model);

            return Redirect("/");
        }
        public IActionResult Creatures()
        {
            var creatures = this.creatureService.CreatureCollection();

            return View(creatures);
        }
        public IActionResult AddAreas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAreas(CreateAreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.areaService.AddAreas(model);

            return Redirect("/");
        }

        public IActionResult Areas()
        {

            var areas = this.areaService.AreasCollection();

            return View(areas);
        }

        public IActionResult Test([FromQuery] string creatureId)
        {
            if (creatureId != null)
            {
                return Json("Done");
            }
            return View();
        }
    }
}
