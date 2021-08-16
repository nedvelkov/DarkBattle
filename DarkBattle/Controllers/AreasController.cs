namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Infrastructure;
    using DarkBattle.Services.Interface;

    using DarkBattle.ViewModels.Areas;
    using Microsoft.AspNetCore.Authorization;

    using static DarkBattle.DarkBattleRoles;

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
