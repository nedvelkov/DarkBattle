namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Consumables;
    using DarkBattle.ViewModels.CreatureConsumables;

    public class CreatureConsumablesService : ICreatureConsumablesService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CreatureConsumablesService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public CreatureConsumableViewModel AddItems(string creatureId)
            => GetListView(x => x.CreatureId == null, creatureId);
        public CreatureConsumableViewModel Items(string creatureId)
            => GetListView(x => x.CreatureId == creatureId, creatureId);

        public bool Add(string consumableId, string creatureId)
        {
            var creature = Creature(creatureId);
            var consumable = Consumable(consumableId);
            if (creature.Consumables.Contains(consumable))
            {
                return false;
            }

            creature.Consumables.Add(consumable);

            this.data.SaveChanges();

            return true;
        }

        public bool Remove(string consumableId, string creatureId)
        {
            var creature = Creature(creatureId);
            var consumable = Consumable(consumableId);
            if (creature.Consumables.Contains(consumable) == false)
            {
                return false;
            }

            creature.Consumables.Remove(consumable);

            this.data.SaveChanges();

            return true;
        }

        private Consumable Consumable(string consumableId)
            => this.data.Consumables.FirstOrDefault(x => x.Id == consumableId);

        private Creature Creature(string creatureId)
            => this.data
                   .Creatures
                   .Include(x => x.Consumables)
                   .FirstOrDefault(x => x.Id == creatureId);

        private CreatureConsumableViewModel GetListView(Func<Consumable, bool> func, string creatureId)
        {
            var creaure = Creature(creatureId);

            var consumables = GetItems<ConsumableViewModel>(func);
            return new CreatureConsumableViewModel()
            {
                CreatureId = creaure.Id,
                CreatureName = creaure.Name,
                Consumables = consumables
            };
        }
        private ICollection<T> GetItems<T>(Func<Consumable, bool> func)
        => this.data.Consumables
                    .Where(func)
                    .OrderBy(x => x.RestoreHealth)
                    .Select(this.mapper.Map<T>)
                    .ToList();
    }
}
