namespace DarkBattle.Services.Models
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;

    public class MerchantConsumablesService : IMerchantConsumablesService
    {
        private readonly ApplicationDbContext data;

        public MerchantConsumablesService(ApplicationDbContext data) 
            => this.data = data;

        public void Add(string consumableId, string merchantId)
        {
            var consumableAsQuarebale = this.data.Consumables.Single(x => x.Id == consumableId);

            consumableAsQuarebale.MerchantId= merchantId;

            this.data.SaveChanges();
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

    }

}
