namespace DarkBattle.ViewModels.MerchantConsumables
{
  
    using System.Collections.Generic;
    
    using DarkBattle.ViewModels.Consumables;
    public class MerchantConsumablesAddViewModel
    {
        public string MerchantId { get; init; }
        public string MerchantName { get; init; }

        public ICollection<ConsumableViewModel> Consumables { get; set; }
    }
}
