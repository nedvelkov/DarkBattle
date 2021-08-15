﻿namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.ChampionClasses;

    public class ChampionClassesController:AdminController
    {
        private readonly IChampionClassService classService;

        public ChampionClassesController (IChampionClassService classService) => this.classService = classService;

        public IActionResult Index() => View(this.classService.ClassCollection());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ChampionClassViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.classService.Add(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(string classId)
        {
            var championClass = this.classService.GetClass(classId);
            if (championClass == null)
            {
                return Redirect("/Home/Error");
            }
            
            return View(championClass);
        }

        [HttpPost]
        public IActionResult Edit(ChampionClassViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.classService.Edit(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string classId)
        {
            if (this.classService.Delete(classId) == false)
            {
                return Redirect("/Home/Error");
            }

            return RedirectToAction("Index");
        }
    }
}
