namespace DarkBattle.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.CreatureItems;
    using DarkBattle.ViewModels.Items;
    using Microsoft.EntityFrameworkCore;


    public class CreatureItemsService : ICreatureItemsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public CreatureItemsService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public CreateureItemViewModel AddItems(string creatureId)
            => GetListView(x => x.CreatureId == null, creatureId);
        public CreateureItemViewModel Items(string creatureId)
            => GetListView(x => x.CreatureId == creatureId, creatureId);

        public bool Add(string itemId, string creatureId)
        {
            var creature = Creature(creatureId);
            var item = Item(itemId);
            if (creature.Items.Contains(item))
            {
                return false;
            }

            creature.Items.Add(item);

            this.data.SaveChanges();

            return true;
        }

        public bool Remove(string itemId, string creatureId)
        {
            var creature = Creature(creatureId);
            var item = Item(itemId);
            if (creature.Items.Contains(item) == false)
            {
                return false;
            }

            creature.Items.Remove(item);

            this.data.SaveChanges();

            return true;
        }

        private Item Item(string itemId)
            => this.data.Items.FirstOrDefault(x => x.Id == itemId);
        private Creature Creature(string creatureId)
            => this.data
                   .Creatures
                   .Include(x => x.Items)
                   .FirstOrDefault(x => x.Id == creatureId);

        private CreateureItemViewModel GetListView(Func<Item, bool> func, string creatureId)
        {
            var creaure = Creature(creatureId);

            var items = GetItems<ItemsListView>(func);
            return new CreateureItemViewModel()
            {
                CreatureId = creaure.Id,
                CreatureName = creaure.Name,
                ItemCollection = items
            };
        }
        private ICollection<T> GetItems<T>(Func<Item, bool> func)
        => this.data.Items
                    .Where(func)
                    .OrderBy(x => x.RequiredLevel)
                    .Select(this.mapper.Map<T>)
                    .ToList();

    }
}
