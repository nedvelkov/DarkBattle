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

        public IActionResult AddCreature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCreature(CreateCreatureViewModel model)
        {
            //TODO : Configurate Db
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
                Expirience = model.Expiriece
            };

            ;

            this.data.Creatures.Add(creature);
            this.data.SaveChanges();

            return Redirect("/");
        }
        public IActionResult Creatures()
        {
            var creatures = new List<CreatureListViewModel>();
            creatures.Add(new CreatureListViewModel
            {
                Name = "Anubis",
                Level = 2,
            });
            return View(creatures);
        }
        public IActionResult AddAreas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAreas(CreateAreaViewModel model)
        {
            //TODO : Configurate Db
            //var area = new Area
            //{
            //    Name = model.Name,
            //    ImageUrl = model.ImageUrl,
            //    Description = model.Description,
            //    MinLevelEnterence = model.MinLevelEnterence,
            //    MaxLevelCreatures = model.MaxLevelCreatures,
            //};
            //this.data.Areas.Add(area);
            //this.data.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Areas()
        {
            //TODO : Configurate Db
            //var areas = this.data
            //    .Areas
            //    .Select(x => new AreasListViewModel
            //    {
            //        Name = x.Name,
            //        Description = x.Description,
            //        CraturesCount = x.Creatures.Count
            //    })
            //    .ToList();

            var areas = new List<AreasListViewModel>();
            areas.Add(new AreasListViewModel
            {
                Id="1",
                Name = "Valey",
                Description = "Beginer teritory",
                MinLevel=1,
                MaxLevel=5,
                CraturesCount=0
            });
            return View(areas);
        }
    }
}
