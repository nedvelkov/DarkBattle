﻿namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Areas;


    public class AreasController : AdminController
    {
        private readonly IAreaService areaService;


        public AreasController(IAreaService areaService)
            => this.areaService = areaService;

        public IActionResult Index()
            => View(this.areaService.AreasCollection());

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(AreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.areaService.Add(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(string areaId)
        {
            var area = this.areaService.GetArea(areaId);
            if (area == null)
            {
                return Redirect("/Home/Error");
            }

            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(AreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.areaService.Edit(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string areaId)
        {
            if (this.areaService.Delete(areaId) == false)
            {
                return Redirect("/Home/Error");
            }

            return RedirectToAction("Index");
        }
    }
}
