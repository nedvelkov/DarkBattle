namespace DarkBattle.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.Services.ServiceModels;
    using DarkBattle.ViewModels.Items;
    using DarkBattle.ViewModels.MerchantItems;
    using Microsoft.EntityFrameworkCore;

    public class MerchantItemsService : IMerchantItemsService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;
        private readonly IChampionService championService;

        public MerchantItemsService(ApplicationDbContext data,
                                    IMapper mapper,
                                    IChampionService championService)
{
            this.data = data;
            this.mapper = mapper;
            this.championService = championService;
        }

        public void Add(string itemId, string merchantId)
        {
            var itemAsQuarebale = this.data.Items.Single(x => x.Id == itemId);

            itemAsQuarebale.MerchantId = merchantId;

            this.data.SaveChanges();
        }

        public MerchantItemAddViewModel Items(string merchantId)
            => GetListView(x => x.MerchantId == null, merchantId);
        

        public MerchantItemPageModel ItemsSellByMerchant(string merchantId)
        {
            var merchant = GetMerchant(merchantId);
            var items = GetItems<ItemViewModel>(x => x.MerchantId == merchantId);

            return new MerchantItemPageModel()
            {
                MerchantId = merchantId,
                MerchantName = merchant.Name,
                ItemCollection = items
            };
        }

        public MerchantItemPageModel SortedItemsSellByMerchant(MerchantItemPageModel model)
        {
            var merchant= this.ItemsSellByMerchant(model.MerchantId);
            if (model.SelectItemLevel != 0)
            {
                merchant.ItemCollection = SortList((x => x.RequiredLevel == model.SelectItemLevel),merchant.ItemCollection);
            }
            if (model.SelectItemType != null)
            {
                merchant.ItemCollection = SortList((x => x.Type == model.SelectItemType), merchant.ItemCollection);
            }
            if(model.SelectObteinBy != null)
            {
                merchant.ItemCollection = SortList((x => x.ObtainBy == model.SelectObteinBy), merchant.ItemCollection);
            }

            return merchant;
        }


        public void Remove(string itemId, string merchantId)
        {
            var itemAsQuarebale = this.data.Items.Single(x => x.Id == itemId);

            if (itemAsQuarebale.MerchantId == merchantId)
            {
                itemAsQuarebale.MerchantId = null;
            }

            this.data.SaveChanges();
        }

        private MerchantItemAddViewModel GetListView(Func<Item, bool> func, string merchantId)
        {
            var merchant = GetMerchant(merchantId);

            var items = GetItems<MerchantItemsListView>(func);
            return new MerchantItemAddViewModel()
            {
                MerchantId = merchant.Id,
                MerchantName = merchant.Name,
                ItemCollection = items
            };
        }

        private ICollection<T> GetItems<T>(Func<Item, bool> func)
                => this.data.Items
                            .Where(func)
                            .OrderBy(x=>x.RequiredLevel)
                            .Select(this.mapper.Map<T>)
                            .ToList();

        private Merchant GetMerchant(string merchantId)
            => this.data.Merchants.FirstOrDefault(x => x.Id == merchantId);

        private ICollection<T> SortList<T>(Func<T, bool> func, ICollection<T> collection)
            => collection.Where(func).ToList();

        public void SellItems(string championId, string merchantId)
        {
            throw new NotImplementedException();
        }
    }
}
