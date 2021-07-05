namespace DarkBattle.Controllers
{
    using DarkBattle.Data;
    using DarkBattle.Models;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Creatures;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class AdminController:Controller
    {
        private readonly DarkBattleDbContext data;
        private readonly IMapper mapper;

        public AdminController(DarkBattleDbContext data,
                                IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public IActionResult AddCreature([FromQuery] string creatureId)
        {
            if (creatureId != null)
            {
                var copyCreature = this.data
                         .Creatures
                         .Where(x => x.Id == creatureId)
                         .ProjectTo<CreateCreatureViewModel>(mapper.ConfigurationProvider)
                         .First();
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
