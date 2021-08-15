namespace DarkBattle.ViewModels.MerchantItems
{
    using DarkBattle.Services.ServiceModels.Items;
    using System.Collections.Generic;


    public class MerchantItemsToAddViewModel
    {
        public string MerchantId { get; init; }
        public string MerchantName { get; init; }

        public ICollection<ItemServiceListModel> ItemCollection { get; set; }
    }
}
