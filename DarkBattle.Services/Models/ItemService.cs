namespace DarkBattle.Services.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using AutoMapper;
    
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Items;
    using DarkBattle.Services.Interface;
  
    public class ItemService:IItemService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ItemService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ItemViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var item = this.mapper.Map<Item>(model);
            this.data.Items.Add(item);
            this.data.SaveChanges();
        }

        public ItemViewModel GetChamponClasses()
        {
            return new ItemViewModel
            {
                ChampionClasses = this.data.ChampionClasses.Select(x => x.Name).ToList()
            };
        }

        public void Edit(ItemViewModel model)
        {
            var item = this.data.Items.Single(x => x.Id == model.Id);

            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    continue;
                }
                var value = prop.GetValue(model);
                var property = item.GetType().GetProperty(prop.Name);
                property.SetValue(item, value);
            }

            this.data.SaveChanges();
        }

        public ItemViewModel GetItem(string id)
        {
            var item = this.mapper
                            .Map<ItemViewModel>
                            (this.GetItemById(id));
            item.ChampionClasses= this.data.ChampionClasses.Select(x => x.Name).ToList();
            return item;
        }

        public ICollection<ItemListViewModel> ItemsCollection()
        {
            var items = this.data
                .Items
                .Select(x => this.mapper.Map<ItemListViewModel>(x))
                .ToList();

            return items;
        }

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
    }
}
