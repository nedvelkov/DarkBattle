namespace DarkBattle.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services;
    using DarkBattle.ViewModels.ChampionClasses;

    public class ChampionClassController:Controller
    {
        private readonly IChampionClassService classService;

        public ChampionClassController(IChampionClassService classService) => this.classService = classService;

        public IActionResult Index()
        {

            return View(this.classService.ClassCollection());
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(ChampionClassViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.classService.Add(model);

            return Redirect("/ChampionClass");
        }

        public IActionResult Edit([FromQuery] string classId)
        {
            var area = this.classService.GetClass(classId);
            if (area == null)
            {
                return Redirect("/Home/Error");
            }

            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(ChampionClassViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return Redirect("/Home/Error");
            }

            this.classService.Edit(model);

            return Redirect("/ChampionClass");
        }

        public IActionResult Delete([FromQuery] string classId)
        {
            if (this.classService.Delete(classId) == false)
            {
                return Redirect("/Home/Error");

            }

            return Redirect("/ChampionClass");
        }
    }
}
