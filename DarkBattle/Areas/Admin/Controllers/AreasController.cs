namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;


    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Areas;


    public class AreasController : AdminController
    {
        private readonly IAreaService areaService;
        private readonly IChampionService championService;

        public AreasController(IAreaService areaService, IChampionService championService)
        {
            this.areaService = areaService;
            this.championService = championService;
        }

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

            return RedirectToAction("/");
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

            return RedirectToAction("/");
        }

        public IActionResult Delete([FromQuery] string areaId)
        {
            if (this.areaService.Delete(areaId) == false)
            {
                return Redirect("/Home/Error");

            }

            return RedirectToAction("/");
        }
    }
}
