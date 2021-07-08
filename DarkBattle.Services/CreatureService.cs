namespace DarkBattle.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
   
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Creatures;

    public class CreatureService : ICreatureService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CreatureService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(CreatureViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var creature = this.mapper.Map<Creature>(model);
            this.data.Creatures.Add(creature);
            this.data.SaveChanges();
        }

        public void Edit(CreatureViewModel model)
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


        public ICollection<CreatureListViewModel> CreaturesCollection()
        {
            var creatures = this.data
                .Creatures
                .ProjectTo<CreatureListViewModel>(mapper.ConfigurationProvider)
                .ToList();

            return creatures;
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


        private Creature GetCreatureById(string id)
        => this.data
            .Creatures
            .FirstOrDefault(x => x.Id == id);
    }
}
