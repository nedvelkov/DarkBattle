namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels.Items;

    public class ItemService : IItemService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ItemService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ItemServiceModel model)
        {
            var item = this.mapper.Map<Item>(model);
            this.data.Items.Add(item);
            this.data.SaveChanges();
        }


        public void Edit(ItemServiceModel model)
        {
            var item = this.data.Items.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id" || prop.Name == "ChampionClasses" || prop.Name == "ItemType")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = item.GetType().GetProperty(prop.Name);
                property.SetValue(item, value);
            }

            this.data.SaveChanges();
        }

        public ItemServiceModel GetItem(string id)
            => this.mapper
                    .Map<ItemServiceModel>
                    (this.GetItemById(id));


        public ICollection<ItemServiceListModel> ItemsCollection()
            => GetItemCollection<ItemServiceListModel>(x => true);

        public ICollection<ItemServiceListModel> CreatureItems(string creatureId)
             => GetItemCollection<ItemServiceListModel>(x => x.CreatureId==creatureId);

        public ICollection<ItemServiceListModel> ItemsWithNoCreature()
            => GetItemCollection<ItemServiceListModel>(x => x.CreatureId == null);

        public ICollection<ItemServiceListModel> ItemsWithNoMerchant()
            => GetItemCollection<ItemServiceListModel>(x => x.MerchantId == null);

        public ICollection<ItemServiceModel> ItemsSellByMerchant(string merchantId)
            => GetItemCollection<ItemServiceModel>(x => x.MerchantId == merchantId);


        public bool Delete(string id)
        {
            var item = GetItemById(id);
            if (item == null)
            {
                return false;
            }

            this.data.Items.Remove(item);

            this.data.SaveChanges();

            return true;
        }

        private Item GetItemById(string id)
        => this.data
            .Items
            .FirstOrDefault(x => x.Id == id);

        private ICollection<T> GetItemCollection<T>(Func<Item,bool> func)
            =>this.data
                    .Items
                    .Where(func)
                    .Select(x => this.mapper.Map<T>(x))
                    .ToList();
    }
}
