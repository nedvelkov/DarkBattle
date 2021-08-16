namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;


    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Consumables;


    public class ConsumableService : IConsumableService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ConsumableService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ConsumableViewServiceModel model)
        {
            var consumable = this.mapper.Map<Consumable>(model);
            this.data.Consumables.Add(consumable);
            this.data.SaveChanges();
        }
        public void Edit(ConsumableViewServiceModel model)
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

        public ConsumableViewServiceModel GetConsumable(string id)
                  =>this.mapper
                        .Map<ConsumableViewServiceModel>
                        (this.GetConsumableById(id));


        public ICollection<ConsumableViewServiceModel> ConsumablesCollection()
            => GetConsumableCollection<ConsumableViewServiceModel>(x => true);

        public ICollection<ConsumableViewServiceModel> ConsumablesWithNoMerchant()
            => GetConsumableCollection<ConsumableViewServiceModel>(x => x.MerchantId == null);

        public ICollection<ConsumableViewServiceModel> ConsumablesSellByMerchant(string merchantId)
            => GetConsumableCollection<ConsumableViewServiceModel>(x => x.MerchantId == merchantId);

        public ICollection<ConsumableViewServiceModel> ConsumablesWithNoCreature()
        => GetConsumableCollection<ConsumableViewServiceModel>(x => x.CreatureId == null);

        public ICollection<ConsumableViewServiceModel> ConsumablesOwnByCreature(string creatureId)
        => GetConsumableCollection<ConsumableViewServiceModel>(x => x.CreatureId == creatureId);


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
        private ICollection<T> GetConsumableCollection<T>(Func<Consumable,bool> func)
                => this.data.Consumables
                            .Where(func)
                            .Select(this.mapper.Map<T>)
                            .ToList();
    }
}
