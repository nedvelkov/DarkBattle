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
    using DarkBattle.ViewModels;
    using System.Diagnostics;

    public class AdminController : Controller
    {
        private readonly ICreatureService creatureService;
        private readonly IAreaService areaService;
        private readonly IValidator validator;

        public AdminController(ICreatureService creatureService,
            IValidator validator,
                                IAreaService areaService)
        {
            this.creatureService = creatureService;
            this.areaService = areaService;
            this.validator = validator;
        }

        public IActionResult Creatures()
        {
            var creatures = this.creatureService.CreatureCollection();

            return View(creatures);
        }

        public IActionResult AddCreature([FromQuery] string creatureId)
        {

            if (creatureId != null)
            {
                var copyCreature = this.creatureService
                                       .GetCreature(creatureId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddCreature(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.creatureService.AddCreature(model);

            return Redirect("/");
        }

        public IActionResult EditCreature([FromQuery] string creatureId)
        {
            var creature = this.creatureService.GetCreature(creatureId);
            if (this.validator.IsValid(creature) == false)
            {
                return Error("Invalid creature model");
            }
            return View(creature);
        }

        [HttpPost]
        public IActionResult EditCreature(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.creatureService.EditCreature(model);

            return Redirect("/");
        }

        //Areas

        public IActionResult Areas()
        {

            var areas = this.areaService.AreasCollection();

            return View(areas);
        }
        public IActionResult AddAreas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAreas(AreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.areaService.AddAreas(model);

            return Redirect("/");
        }

        public IActionResult EditArea([FromQuery] string areaId)
        {
            var area = this.areaService.GetArea(areaId);
            if (area == null)
            {
                return Error("Invalid area model");
            }

            return View(area);
        }
        [HttpPost]
        public IActionResult EditArea(AreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return Error("Invalid area model");
            }

            this.areaService.EditArea(model);

            return Redirect("/Admin/Areas");
        }

        public IActionResult DeleteArea([FromQuery] string id)
        {
            this.areaService.DeleteArea(id);

            return Redirect("/Admin/Areas");
        }




        public IActionResult Test([FromQuery] string id)
        {
            if (id != null)
            {
                return Redirect("/");
            }
            return View();
        }

        public IActionResult Error(string error)
        {
            return View(error);
        }


    }
}
