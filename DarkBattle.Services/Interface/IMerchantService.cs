namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Merchants;

    public  interface IMerchantService
    {
        public void Add(MerchantServiceModel model);

        public void Edit(MerchantServiceModel model);

        public MerchantServiceModel GetMerchant(string merchantId);

        public ICollection<MerchantServiceListModel> MerchantsCollection();
        public string MerchantName(string merchantId);

        public bool Delete(string id);

        public MerchantMarketViewModel AllMerchants(string championId, string playerId);

    }
}
