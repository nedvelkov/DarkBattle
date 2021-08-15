namespace DarkBattle.Services.Models
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;



    public class MerchantItemsService : IMerchantItemsService
    {
        private readonly ApplicationDbContext data;

        public MerchantItemsService(ApplicationDbContext data)
                => this.data = data;

        public void Add(string itemId, string merchantId)
        {
            var itemAsQuarebale = this.data.Items.Single(x => x.Id == itemId);

            itemAsQuarebale.MerchantId = merchantId;

            this.data.SaveChanges();
        }

        public bool SellItem(string championId, string itemId)
        {
            var champion = this.data
                                .Champions
                                .Include(x => x.Items)
                                .Single(x => x.Id == championId);
            var item = this.data.Items.Single(x => x.Id == itemId);
            var cost = item.Value;
            if (cost > champion.Gold)
            {
                return false;
            }
            if (champion.Items.Any(x => x.Id == itemId))
            {
                return false;
            }
            champion.Items.Add(item);
            champion.Gold -= cost;
            this.data.SaveChanges();

            return true;
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

    }
}
