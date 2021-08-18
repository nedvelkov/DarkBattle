namespace DarkBattle.Services.ServiceModels.Merchants
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Champions;

    public class MerchantMarketViewModel
    {

        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<MerchantChampionViewModel> Merchants { get; set; }


    }
}
