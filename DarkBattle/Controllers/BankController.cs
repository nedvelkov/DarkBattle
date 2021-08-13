namespace DarkBattle.Controllers
{
    using DarkBattle.Services.Interface;
    using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction("Index", "Champions");
            }

            this.bankService.GetLoan(championId);
            return RedirectToAction("Details", "Champions",new { championId=$"{championId}"});

        }
    }
}
