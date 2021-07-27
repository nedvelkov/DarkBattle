﻿namespace DarkBattle.Services.Interface
{

    using DarkBattle.ViewModels.MerchantItems;

   public interface IMerchantItemsService
    {
        public void Add(string itemId, string merchantId);
        public void Remove(string itemId, string merchantId);

        public MerchantItemAddViewModel Items(string merchantId);

        public MerchantItemPageModel ItemsSellByMerchant(string merchantId);
        public MerchantItemPageModel SortedItemsSellByMerchant(MerchantItemPageModel model);


    }
}