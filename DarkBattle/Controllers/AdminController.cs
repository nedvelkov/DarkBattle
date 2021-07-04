using DarkBattle.Data;
using DarkBattle.Data.Models;
using DarkBattle.Models;
using DarkBattle.Models.Areas;
using DarkBattle.Models.Creatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBattle.Controllers
{
    public class AdminController:Controller
    {
        private readonly DarkBattleDbContext data;

        public AdminController(DarkBattleDbContext data) => this.data = data;

        public IActionResult AddCreature([FromQuery] string creatureId)
        {
            if (creatureId != null)
            {
                var copyCreature = this.data
                         .Creatures
                         .Where(x => x.Id == creatureId)
                         .Select(x => new CreateCreatureViewModel
                         {
                             Name = x.Name,
                             Image = x.ImageUrl,
                             Attack = x.Attack,
                             Defense = x.Defense,
                             Health = x.Health,
                             Block = x.Block,
                             Level = x.Level,
                             Gold = x.Gold,
                             Expirience = x.Expirience
                         }).First();
                return View(copyCreature);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCreature(CreateCreatureViewModel model)
        {
            if (this.ModelState.IsValid==false)
            {
                return View(model);
            }
            var creature = new Creature
            {
                Name = model.Name,
                ImageUrl = model.Image,
                Attack = model.Attack,
                Defense = model.Defense,
                Health = model.Health,
                Block = model.Block,
                Level = model.Level,
                Gold = model.Gold,
                Expirience = model.Expirience
            };

            this.data.Creatures.Add(creature);
            this.data.SaveChanges();

            return Redirect("/");
        }
        public IActionResult Creatures()
        {
            var creatures=  this.data
                .Creatures
                .Select(x => new CreatureListViewModel
                {
                    Id=x.Id,
                    Name = x.Name,
                    Level = x.Level,
                    Area = x.Area.Name
                })
                .ToList();
            return View(creatures);
        }
        public IActionResult AddAreas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAreas(CreateAreaViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(model);
            }
            var area = new Area
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                MinLevelEnterence = model.MinLevelEnterence,
                MaxLevelCreatures = model.MaxLevelCreatures,
            };
            this.data.Areas.Add(area);
            this.data.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Areas()
        {
            //TODO : Configurate Db
            var areas = this.data
                .Areas
                .Select(x => new AreasListViewModel
                {
                    Name = x.Name,
                    MinLevel=x.MinLevelEnterence,
                    MaxLevel=x.MaxLevelCreatures,
                    Description = x.Description,
                    CraturesCount = x.Creatures.Count
                })
                .ToList();
            return View(areas);
        }

        public IActionResult Test([FromQuery]string creatureId)
        {
            if (creatureId != null)
            {
                return Json("Done");
            }
            return View();
        }
    }
}
