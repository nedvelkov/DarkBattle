namespace DarkBattle.Services.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class MerchantMarketViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<MerchantServiceModel> Merchants {get;set;}
    }
}
