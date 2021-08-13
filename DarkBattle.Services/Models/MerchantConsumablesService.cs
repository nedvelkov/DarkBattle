namespace DarkBattle.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;
    using DarkBattle.ViewModels.Consumables;
    using DarkBattle.ViewModels.Enums;
    using DarkBattle.ViewModels.MerchantConsumables;
    using Microsoft.EntityFrameworkCore;

    public class MerchantConsumablesService : IMerchantConsumablesService
    {
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;

        public MerchantConsumablesService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
        public void Add(string consumableId, string merchantId)
        {
            var consumableAsQuarebale = this.data.Consumables.Single(x => x.Id == consumableId);

            consumableAsQuarebale.MerchantId= merchantId;

            this.data.SaveChanges();
        }

        public MerchantConsumablesAddViewModel Consumables(string merchantId)
        {
            var merchant = GetMerchant(merchantId);
            var consumables = GetConsumables<ConsumableViewModel>(x => x.MerchantId == null);

            return new MerchantConsumablesAddViewModel
            {
                MerchantId = merchantId,
                MerchantName = merchant.Name,
                Consumables = consumables
            };
        }

        public void Remove(string consumableId, string merchantId)
        {
            var consumableAsQuarebale = this.data.Consumables.Single(x => x.Id == consumableId);
            if (consumableAsQuarebale.MerchantId==merchantId)
            {

                consumableAsQuarebale.MerchantId = null;
            }

            this.data.SaveChanges();
        }

        public bool SellConsumable (string championId, string itemId)
        {
            var champion = this.data
                                .Champions
                                .Include(x => x.ChampionConsumables)
                                .Single(x => x.Id == championId);
            var consumabe = this.data.Consumables.Single(x => x.Id == itemId);
            var cost = consumabe.Value;
            if (cost > champion.Gold)
            {
                return false;
            }
            if (champion.ChampionConsumables.Any(x => x.Id == itemId))
            {
                return false;
            }
            champion.ChampionConsumables.Add(consumabe);
            champion.Gold -= cost;
            this.data.SaveChanges();

            return true;
        }

        public MerchantConsumablePageModel ConsumablesSellByMerchant(string merchantId)
        {
            var merchant = GetMerchant(merchantId);
            var consumables = GetConsumables<ConsumableViewModel>(x => x.MerchantId == merchantId);

            return new MerchantConsumablePageModel()
            {
                MerchantId = merchantId,
                MerchantName = merchant.Name,
                Consumables = consumables
            };
        }
        public MerchantConsumablePageModel SortedConsumablesSellByMerchant(MerchantConsumablePageModel model)
        {
            var merchant = this.ConsumablesSellByMerchant(model.MerchantId);
            if (model.SearchByName != null)
            {
                merchant.Consumables = merchant.Consumables
                                        .Where(x => x.Name
                                                     .ToLower()
                                                     .Contains(model.SearchByName.ToLower()))
                                        .ToList();
            }

            merchant.Consumables = model.Sorting switch
            {
                ConsumableSorting.Value => merchant.Consumables.OrderByDescending(x => x.Value).ToList(),
                ConsumableSorting.Healt or _ => merchant.Consumables.OrderByDescending(x => x.RestoreHealth).ToList(),
            };

            return merchant;
        }


        private ICollection<T> GetConsumables<T>(Func<Consumable, bool> func)
                => this.data.Consumables
                            .Where(func)
                            .OrderBy(x => x.RestoreHealth)
                            .Select(this.mapper.Map<T>)
                            .ToList();

        private Merchant GetMerchant(string merchantId)
            => this.data.Merchants.FirstOrDefault(x => x.Id == merchantId);

        private ICollection<T> SortList<T>(Func<T, bool> func, ICollection<T> collection)
            => collection.Where(func).ToList();
    }

}
