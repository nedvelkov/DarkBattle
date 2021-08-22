namespace DarkBattle.Controllers
{
    using DarkBattle.Services.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static DarkBattle.DarkBattleRoles;
    using static DarkBattle.WebStatistics;

    [Authorize(Roles = PlayerRoleName)]

    public class BankController : Controller
    {
        private readonly IBankService bankService;

        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        public IActionResult GetLoan(string championId)
        {
            if (championId == null)
            {
                return RedirectToAction("Index", "Champions", new { error = SelectChampionError });

            }

            this.bankService.GetLoan(championId);
            return RedirectToAction("Details", "Champions",new { championId=$"{championId}"});

        }
    }
}
