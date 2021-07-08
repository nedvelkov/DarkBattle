namespace DarkBattle.Controllers
{
    using DarkBattle.Services;
    using DarkBattle.ViewModels.Areas;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class AreaController : Controller
    {
        private readonly IAreaService areaService;

        public AreaController(IAreaService areaService) => this.areaService = areaService;

        public IActionResult Index()
        {

            return View(this.areaService.AreasCollection());
        }

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

            return Redirect("/Area/Index");
        }

        public IActionResult Edit([FromQuery] string areaId)
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
                return Redirect("/Home/Error");
            }

            this.areaService.Edit(model);

            return Redirect("/Area");
        }

        public IActionResult Delete([FromQuery] string areaId)
        {
            if (this.areaService.Delete(areaId) == false)
            {
                return Redirect("/Home/Error");

            }

            return Redirect("/Area");
        }
    }
}
