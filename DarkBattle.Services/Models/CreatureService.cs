namespace DarkBattle.Services.Models
{
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Creatures;
    using System;

    public class CreatureService : ICreatureService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CreatureService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(CreatureServiceModel model)
        {
            var creature = this.mapper.Map<Creature>(model);
            this.data.Creatures.Add(creature);
            this.data.SaveChanges();
        }

        public void Edit(CreatureServiceModel model)
        {
            var creature = this.data.Creatures.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = creature.GetType().GetProperty(prop.Name);
                property.SetValue(creature, value);
            }

            this.data.SaveChanges();
        }

        public CreatureServiceModel GetCreature(string id)
        {
            var creature = this.mapper
                                    .Map<CreatureServiceModel>
                                    (this.GetCreatureById(id));

            return creature;
        }


        public ICollection<CreatureServiceListModel> CreaturesCollection()
            => GetCreatureCollection<CreatureServiceListModel>(x => true);

        public ICollection<CreateureInAreaServiceModel> CreatureWithNoArea(int minLevel, int maxLevel)
            => GetCreatureCollection<CreateureInAreaServiceModel>(x => x.AreaId == null && x.Level >= minLevel && x.Level <= maxLevel);

        public ICollection<CreateureInAreaServiceModel> CreatureInArea(string areaId)
            => GetCreatureCollection<CreateureInAreaServiceModel>(x => x.AreaId == areaId);


        public bool Delete(string id)
        {
            var creature = GetCreatureById(id);
            if (creature == null)
            {
                return false;
            }

            this.data.Creatures.Remove(creature);

            this.data.SaveChanges();

            return true;
        }

        public string CreatureName(string creatureId)
            => this.data.Creatures.FirstOrDefault(x => x.Id == creatureId).Name;

        private Creature GetCreatureById(string id)
        => this.data
            .Creatures
            .FirstOrDefault(x => x.Id == id);
        private ICollection<T> GetCreatureCollection<T>(Func<Creature, bool> func)
                => this.data
                        .Creatures
                        .Where(func)
                        .Select(x => this.mapper.Map<T>(x))
                        .ToList();
    }
}
