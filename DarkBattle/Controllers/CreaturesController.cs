﻿namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Creatures;

    public class CreaturesController:Controller
    {
        private readonly ICreatureService creatureService;

        public CreaturesController(ICreatureService creatureService) => this.creatureService = creatureService;

        public IActionResult Index()
        {
            
            return View(this.creatureService.CreaturesCollection());
        }

        public IActionResult Create([FromQuery] string creatureId)
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
        public IActionResult Create(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.creatureService.Add(model);

            return Redirect("/Creatures");
        }

        public IActionResult Edit([FromQuery] string creatureId)
        {
            var creature = this.creatureService.GetCreature(creatureId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.creatureService.Edit(model);

            return Redirect("/Creatures");
        }

        public IActionResult Delete([FromQuery] string creatureId)
        {
            if (this.creatureService.Delete(creatureId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Creatures");

        }

    }
}