namespace DarkBattle.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Infrastructure;
    using DarkBattle.Services.Interface;

    using DarkBattle.ViewModels.Areas;
    using Microsoft.AspNetCore.Authorization;

    using static DarkBattle.DarkBattleRoles;
    using static DarkBattle.WebStatistics;

    [Authorize(Roles = PlayerRoleName)]

    public class AreasController : Controller
    {
        private readonly IAreaService areaService;
        private readonly IChampionService championService;



        public AreasController(IAreaService areaService, IChampionService championService)
        {
            this.areaService = areaService;
            this.championService = championService;
        }

        public IActionResult BattleZones(string championId,string page)
        {

            if (championId == null)
            {
                return RedirectToAction("Index", "Champions", new {error=SelectChampionError });
            }

            var playerId = this.User.GetId();

            var model = new BattleZoneViewModel
            {
                Champion = this.championService.ChampionBar(championId, playerId),
                Areas = this.areaService.AreaServiceCollection().OrderBy(x => x.MinLevelEnterence).ToList(),
                MaxPages=this.areaService.AreaServiceCollection().Count
            };
            if (page != null)
            {
                model.CurrentPage = int.Parse(page);
            }
            model.Areas = model.Areas.Skip(model.CurrentPage - 1).Take(model.MaxAreasPerPage).ToList();

            return View(model);

        }
    }
}
