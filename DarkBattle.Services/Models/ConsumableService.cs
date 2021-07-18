namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Consumables;

    public class ConsumableService : IConsumableService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ConsumableService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ConsumableViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var consumable = this.mapper.Map<Consumable>(model);
            this.data.Consumables.Add(consumable);
            this.data.SaveChanges();
        }
        public void Edit(ConsumableViewModel model)
        {
            var consumable = this.data.Consumables.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = consumable.GetType().GetProperty(prop.Name);
                property.SetValue(consumable, value);
            }

            this.data.SaveChanges();
        }

        public ConsumableViewModel GetConsumable(string id)
        {
            var consumable = this.mapper
                        .Map<ConsumableViewModel>
                        (this.GetConsumableById(id));

            return consumable;
        }

        public ICollection<ConsumableListViewModel> ConsumablesCollection()
        {
            var consumables = this.data
                            .Consumables
                            .ProjectTo<ConsumableListViewModel>(mapper.ConfigurationProvider)
                            .ToList();

            return consumables;
        }

        public bool Delete(string id)
        {
            var consumable = GetConsumableById(id);
            if (consumable == null)
            {
                return false;
            }

            this.data.Consumables.Remove(consumable);

            this.data.SaveChanges();

            return true;
        }

        private Consumable GetConsumableById(string id)
                        => this.data
                            .Consumables
                            .FirstOrDefault(x => x.Id == id);
    }
}
