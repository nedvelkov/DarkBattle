namespace DarkBattle.ViewModels.MerchantConsumables
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Consumables;
    
    public class MerchantConsumablesAddViewModel
    {
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }

       public ICollection<ConsumableViewServiceModel> Consumables { get; set; }
    }
}
