namespace DarkBattle.Services
{
    using DarkBattle.ViewModels.Creatures;
    using DarkBattle.Data;
    using AutoMapper;
    using DarkBattle.Data.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
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

        public void AddCreature(CreatureViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var creature = this.mapper.Map<Creature>(model);
            this.data.Creatures.Add(creature);
            this.data.SaveChanges();
        }

        public void EditCreature(CreatureViewModel model)
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

        public CreatureViewModel GetCreature(string id)
        {
            var creature = this.mapper
                                    .Map<CreatureViewModel>
                                    (this.GetCreatureById(id));

            return creature;
        }

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

        public ICollection<CreatureListViewModel> CreatureCollection()
        {
            var creatures = this.data
                .Creatures
                .ProjectTo<CreatureListViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return creatures;
        }


        private Creature GetCreatureById(string id)
        => this.data
            .Creatures
            .FirstOrDefault(x => x.Id == id);
    }
}
