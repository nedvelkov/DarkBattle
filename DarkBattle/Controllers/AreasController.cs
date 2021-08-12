namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Infrastructure;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.Services.ServiceModels;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = "Admin,Player")]

    public class AreasController : Controller
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

            return Redirect("/Areas");
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

            return Redirect("/Areas");
        }

        public IActionResult Delete([FromQuery] string areaId)
        {
            if (this.areaService.Delete(areaId) == false)
            {
                return Redirect("/Home/Error");

            }

            return Redirect("/Areas");
        }

        [Authorize(Roles = "Player")]

        public IActionResult BattleZones(string championId)
        {

            if (championId == null)
            {
                return RedirectToAction("Index", "Champions");
            }

            var playerId = this.User.GetId();

            var model = new BattleZoneViewModel
            {
                Champion = this.championService.ChampionBar(championId, playerId),
                Areas = this.areaService.AreaServiceCollection()
            };
            return View(model);

        }
    }
}
