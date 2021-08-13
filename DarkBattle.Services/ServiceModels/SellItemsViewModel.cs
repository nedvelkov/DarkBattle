namespace DarkBattle.Services.ServiceModels
{
    using DarkBattle.ViewModels.MerchantItems;

    public class SellItemsViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantItemPageModel Items { get; init; }
    }
}
