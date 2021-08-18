namespace DarkBattle.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Creatures;

    using static DarkBattle.Areas.Admin.AdminConstants;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(Roles = AdministratorRoleName)]
    public class CreaturesController:AdminController
    {
        private readonly ICreatureService creatureService;

        public CreaturesController(ICreatureService creatureService) => this.creatureService = creatureService;

        public IActionResult Index()
        {
            
            return View(this.creatureService.CreaturesCollection());
        }

        public IActionResult Create(string creatureId)
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
        public IActionResult Create(CreatureServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }

            this.creatureService.Add(model);

            return RedirectToAction("Index");

        }

        public IActionResult Edit(string creatureId)
        {
            var creature = this.creatureService.GetCreature(creatureId);

            if (creature == null)
            {
                return Redirect("/Home/Error");
            }

            return View(creature);
        }

        [HttpPost]
        public IActionResult Edit(CreatureServiceModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View();
            }

            this.creatureService.Edit(model);

            return RedirectToAction("Index");

        }

        public IActionResult Delete(string creatureId)
        {
            if (this.creatureService.Delete(creatureId) == false)
            {
                return Redirect("/Home/Error");
            }
            return RedirectToAction("Index");

        }

    }
}
