namespace DarkBattle.ViewModels.Merchants
{
    using DarkBattle.ViewModels.MerchantConsumables;
    using DarkBattle.Services.ServiceModels.Champions;

    public class SellConsumablesViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantConsumablePageModel Consumables { get; init; }
    }
}
