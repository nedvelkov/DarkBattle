namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels;
    using DarkBattle.ViewModels.Merchants;

    public  interface IMerchantService
    {
        public void Add(MerchantViewModel model);

        public void Edit(MerchantViewModel model);

        public MerchantViewModel GetMerchant(string id);

        public ICollection<MerchantListViewModel> MerchantsCollection();

        public bool Delete(string id);

        public MerchantMarketViewModel AllMerchants(string championId, string playerId);

    }
}
