namespace DarkBattle.ViewModels.Merchants
{

    using DarkBattle.Services.ServiceModels.Champions;
    using DarkBattle.ViewModels.MerchantItems;
    public class SellItemsViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantItemPageModel Items { get; init; }
    }
}
