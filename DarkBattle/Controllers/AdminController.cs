﻿namespace DarkBattle.Controllers
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
        public IActionResult AddCreature(CreatureViewModel model)
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
        public IActionResult AddAreas(AreaViewModel model)
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

        public IActionResult EditArea([FromQuery] string areaId)
        {
                var area = this.areaService.GetArea(areaId);
                if (area == null)
                {
                    //TODO : Error invalid area
                    return Json(areaId);
                }

                return View(area);
        }
        [HttpPost]
        public IActionResult EditArea(AreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                //TODO : Error invalid data
                return Json(this.ModelState);
            }

            this.areaService.EditArea(model);

            return Redirect("/");
        }

        public IActionResult Test([FromQuery] string id)
        {
            if (id != null)
            {
                return Json("Done");
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}
