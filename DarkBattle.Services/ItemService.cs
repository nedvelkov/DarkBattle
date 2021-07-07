﻿namespace DarkBattle.Services
{
    using AutoMapper;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.ViewModels.Items;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ItemService:IItemService
    {

        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public ItemService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void AddItem(ItemViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var item = this.mapper.Map<Item>(model);
            this.data.Items.Add(item);
            this.data.SaveChanges();
        }

        public void EditItem(ItemViewModel model)
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
            return item;
        }

        public ICollection<ItemListViewModel> ItemCollection()
        {
            var items = this.data
                .Items
                .Select(x => this.mapper.Map<ItemListViewModel>(x))
                .ToList();

            return items;
        }

        public bool DeleteItem(string id)
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