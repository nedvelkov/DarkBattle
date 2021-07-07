namespace DarkBattle.Controllers
{
    using DarkBattle.Services;
    using DarkBattle.ViewModels.Creatures;
    using Microsoft.AspNetCore.Mvc;

    public class CreatureController:Controller
    {
        private readonly ICreatureService creatureService;

        public CreatureController(ICreatureService creatureService) => this.creatureService = creatureService;

        public IActionResult Index()
        {
            
            return View(this.creatureService.CreatureCollection());
        }

        public IActionResult Create([FromQuery] string creatureId)
        {
            if (creatureId != null)
            {
                var copyCreature = this.creatureService
                                       .GetCreature(creatureId);
                return View(copyCreature);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.creatureService.AddCreature(model);

            return Redirect("/Creature");
        }

        public IActionResult Edit([FromQuery] string creatureId)
        {
            var creature = this.creatureService.GetCreature(creatureId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(CreatureViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.creatureService.EditCreature(model);

            return Redirect("/Creature");
        }

        public IActionResult Delete([FromQuery] string creatureId)
        {
            if (this.creatureService.Delete(creatureId) == false)
            {
                return Redirect("/Home/Error");
            }
            return Redirect("/Creature");

        }

    }
}
