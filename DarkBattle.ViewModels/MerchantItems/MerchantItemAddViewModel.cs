namespace DarkBattle.ViewModels.MerchantItems
{

    using System.Collections.Generic;

    using DarkBattle.ViewModels.Items;

    public class MerchantItemAddViewModel
    {
        public string MerchantId { get; init; }
        public string MerchantName { get; init; }

        public ICollection<MerchantItemsListView> ItemCollection { get; set; }
    }
}
