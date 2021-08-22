namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Infrastructure;
    using DarkBattle.Services.Interface;


    using static DarkBattle.DarkBattleRoles;
    using static DarkBattle.WebStatistics;

    [Authorize(Roles = PlayerRoleName)]
    public class BattlesController : Controller
    {
        private readonly IBattleService battleService;

        public BattlesController(IBattleService battleService)
        {
            this.battleService = battleService;
        }

        public IActionResult FightCreature(string championId, string areaId)
        {
            var playerId = this.User.GetId();
            var result = this.battleService.FightWithCreature(championId, areaId, playerId);
            return View(result);
        }

        public IActionResult FightInDungeon(string championId, string areaId)
        {
            var playerId = this.User.GetId();
            var result = this.battleService.FightWithCreature(championId, areaId, playerId, true);
            return View(result);
        }

        public IActionResult Training(string championId, string gold)
        {
            if (championId == null)
            {
                return RedirectToAction("Index", "Champions", new { error = SelectChampionError });
            }

            var playerId = this.User.GetId();

            return View(this.battleService.TrainChampion(championId, playerId, gold));
        }
    }
}
