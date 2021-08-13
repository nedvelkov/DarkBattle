namespace DarkBattle.Services.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DarkBattle.ViewModels.MerchantConsumables;
    public interface IMerchantConsumablesService
    {
        public void Add(string consumableId, string merchantId);

        public void Remove(string consumableId, string merchantId);

        public MerchantConsumablesAddViewModel Consumables(string merchantId);

        public MerchantConsumablePageModel ConsumablesSellByMerchant(string merchantId);

        public MerchantConsumablePageModel SortedConsumablesSellByMerchant(MerchantConsumablePageModel model);
        public bool SellConsumable(string championId, string itemId);


    }
}
