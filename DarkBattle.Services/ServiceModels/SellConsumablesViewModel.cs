namespace DarkBattle.Services.ServiceModels
{
    using DarkBattle.ViewModels.MerchantConsumables;
    public class SellConsumablesViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantConsumablePageModel Consumables { get; init; }
    }
}
