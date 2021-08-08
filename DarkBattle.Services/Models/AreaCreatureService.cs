namespace DarkBattle.Services.Models
{

    using System.Linq;

    using AutoMapper;


    using DarkBattle.Data;
    using DarkBattle.ViewModels.AreaCreatures;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Creatures;
    using DarkBattle.Services.Interface;
    using DarkBattle.Data.Models;
    using System;
    using DarkBattle.ViewModels.Enums;

    public class AreaCreatureService : IAreaCreatureService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public AreaCreatureService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(string creatureId, string areaId)
        {
            var creatureAsQuareable = this.data.Creatures.Single(x => x.Id == creatureId);

            creatureAsQuareable.AreaId = areaId;

            this.data.SaveChanges();
        }

        public void Remove(string creatureId, string areaId)
        {
            var creatureAsQuareable = this.data.Creatures.Single(x => x.Id == creatureId);
            if (creatureAsQuareable.AreaId==areaId)
            {
            creatureAsQuareable.AreaId = null;
            }

            this.data.SaveChanges();
        }


        public AreaCreatureAddViewModel ListAllAvilableCreatures(string areaId)
        {
            var area = this.GetArea(areaId);

            Func<Creature, bool> func = x => x.Level >= area.MinLevelEnterence &&
                                           x.Level <= area.MaxLevelCreatures &&
                                            x.AreaId == null;

            return this.GetCreatures(area, func);
        }

        public AreaCreaturesPageModel ListAllCreaturesInArea(string areaId)
        {
            var area = this.GetArea(areaId);

            Func<Creature, bool> func = x => x.AreaId == areaId;

            var model= this.GetCreatures(area, func);
            model.Area.MinLevel = area.MinLevelEnterence;
            model.Area.MaxLevel = area.MaxLevelCreatures;
            return new AreaCreaturesPageModel()
            {
                Area = model.Area,
                Creatures = model.Creatures
            };
        }

        public AreaCreaturesPageModel SortCreatures(AreaCreaturesPageModel model)
        {
            var listCreatures = this.ListAllCreaturesInArea(model.Area.Id);

            if (model.SearchTerm != null)
            {
                listCreatures.Creatures = listCreatures.Creatures
                                                        .Where(x => x.Name
                                                                     .ToLower()
                                                                     .Contains(model.SearchTerm.ToLower()))
                                                        .ToList();
            }

            listCreatures.Creatures = model.Sorting switch
            {
                CreatureSorting.Attack=>listCreatures.Creatures.OrderByDescending(x=>x.Attack).ToList(),
                CreatureSorting.Expirience=>listCreatures.Creatures.OrderByDescending(x=>x.Expirience).ToList(),
                CreatureSorting.Name=>listCreatures.Creatures.OrderByDescending(x=>x.Name).ToList(),
                CreatureSorting.Level or _=>listCreatures.Creatures.OrderByDescending(x=>x.Level).ToList(),
            };

            if (model.CurrentLevel != null)
            {
                listCreatures.Creatures = listCreatures.Creatures
                                                        .Where(x => x.Level == model.CurrentLevel)
                                                        .ToList();
            }

            return listCreatures;
        }

        private AreaCreatureAddViewModel GetCreatures(Area area,Func<Creature,bool> func)
        {
            var creatures = this.data.Creatures
                                .Where(func)
                                .Select(this.mapper.Map<CreatureAreaViewModel>)
                                .ToList();

            var areaMapped = this.mapper.Map<AreaCreatureViewModel>(area);

            return new AreaCreatureAddViewModel
            {
                Area = areaMapped,
                Creatures = creatures
            };
        }

        private Area GetArea(string areaId)
            => this.data.Areas.FirstOrDefault(x => x.Id == areaId);
    }
}
