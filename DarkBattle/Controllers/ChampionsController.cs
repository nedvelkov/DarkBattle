namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Champions;
    using DarkBattle.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Authorization;

    using static DarkBattle.DarkBattleRoles;
    using System.Linq;

    [Authorize(Roles = PlayerRoleName)]
    public class ChampionsController : Controller
    {
        private readonly IChampionService championService;
        private IConfiguration cofing;


        public ChampionsController(IChampionService championService, IConfiguration cofing)
        {
            this.championService = championService;
            this.cofing = cofing;
        }

        public IActionResult Index()
        {
            ViewBag.MaxChampions = int.Parse(this.cofing.GetSection("GameSettings").GetSection("MaxChampions").Value);
            var playerId = this.User.GetId();
            var champions = this.championService.ChampionCollection(playerId);
            return View(champions);
        }

        public IActionResult Create()
        {

            return View(new ChampionViewModel
            {
                ChampionClasses = this.championService.GetChampionClasses().ToList()
            });
        }

        [HttpPost]
        public IActionResult Create(string name, string championClassId)
        {
            var playerId = this.User.GetId();
            if (this.championService.CreateChampion(name, championClassId, playerId))
            {
                return RedirectToAction("Index");
            }
            return Redirect("Create");
        }

        public IActionResult Delete(string championId)
        {

            var playerId = this.User.GetId();

            var deleted = this.championService.DeleteChampion(championId, playerId);

            return RedirectToAction("Index");
        }

        public IActionResult Details(string championId)
        {
            var playerId = this.User.GetId();
            var model = this.championService.Details(championId, playerId);
            return View(model);
        }

        public IActionResult Inventory(string championId)
        {
            var playerId = this.User.GetId();
            var model = this.championService.Details(championId, playerId);
            return View(model);
        }

        public IActionResult EquipItem(string championId,string itemId)
        {
            var result = this.championService.EquipItem(championId, itemId);
            return RedirectToAction("Inventory", new { championId = $"{championId}" });
        }

        public IActionResult RemoveItem(string championId, string itemId)
        {
            var result = this.championService.RemoveItem(championId, itemId);
            return RedirectToAction("Inventory", new { championId = $"{championId}" });
        }

        public IActionResult SellItem(string championId, string itemId)
        {
            var result = this.championService.SellItem(championId, itemId);
            return RedirectToAction("Inventory", new { championId = $"{championId}" });
        }

        public IActionResult Consume(string championId, string consumableId)
        {
            var result = this.championService.Consume(championId, consumableId);

            return RedirectToAction("Inventory", new { championId = $"{championId}" });
        }

    }
}
