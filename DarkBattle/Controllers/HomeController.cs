namespace DarkBattle.Controllers
{
    using DarkBattle.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DarkBattle.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Home;

    using static WebStatistics;

    public class HomeController : Controller
    {
        private readonly IStatisticService statitstic;

        public HomeController( IStatisticService statitstic)
        {
            this.statitstic = statitstic;
        }
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Presentation");
            }
            return View();
        }

        public IActionResult Privacy() => View();

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
                ImageUrl = "https://images2.alphacoders.com/123/123862.jpg"
            });

            presentation.Add(new PresentationViewModel
            {
                SlideName = nameof(WebStatistics.Areas),
                Description = WebStatistics.Areas,
                Statistic = "Conquare all",
                StatisticValue = $"{areas} battle zones",
                ImageUrl = "http://wallpoper.com/images/00/22/25/59/dark-battle_00222559.jpg"
            });

            presentation.Add(new PresentationViewModel
            {
                SlideName = nameof(WebStatistics.ChooseClass),
                Description = WebStatistics.ChooseClass,
                Statistic = "Chosse from",
                StatisticValue = $"{championClasses} unique champions",
                ImageUrl = "https://mmoculture.com/wp-content/uploads/2014/08/MU-Classic-characters.jpg"
            });

            return View(presentation);
        }
    }
}
