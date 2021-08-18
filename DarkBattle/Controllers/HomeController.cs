namespace DarkBattle.Controllers
{


    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using DarkBattle.ViewModels;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Home;

    using static DarkBattleRoles;


    public class HomeController : Controller
    {
        private readonly IStatisticService statitstic;

        public HomeController( IStatisticService statitstic)
        {
            this.statitstic = statitstic;
        }
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated==false)
            {
                return RedirectToAction(nameof(this.Presentation));
            }
            if (this.User.IsInRole(PlayerRoleName))
            {
                return RedirectToAction("Index", "Champions");
            }
            if (this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        public IActionResult About() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult Presentation()
        {
            var presentation = new List<PresentationViewModel>();
            var online = this.statitstic.TotalOnlinePlayers();
            var championClasses = this.statitstic.TotalChampionClass();
            var areas = this.statitstic.TotalAreas();

            presentation.Add(new PresentationViewModel
            {
                SlideName = nameof(WebStatistics.Presentation),
                Description = WebStatistics.Presentation,
                Statistic="Online players",
                StatisticValue=online.ToString(),
                ImageUrl = "https://wallpapercave.com/wp/wp2445551.jpg"
            });

            presentation.Add(new PresentationViewModel
            {
                SlideName = nameof(WebStatistics.Areas),
                Description = WebStatistics.Areas,
                Statistic = "Conquare all",
                StatisticValue = $"{areas} battle zones",
                ImageUrl = "https://wallpapercave.com/wp/wp2445559.jpg"
            });

            presentation.Add(new PresentationViewModel
            {
                SlideName = nameof(WebStatistics.ChooseClass),
                Description = WebStatistics.ChooseClass,
                Statistic = "Chosse from",
                StatisticValue = $"{championClasses} unique champions",
                ImageUrl = "https://wallpapercave.com/wp/wp2445587.png"
            });

            return View(presentation);
        }
    }
}
