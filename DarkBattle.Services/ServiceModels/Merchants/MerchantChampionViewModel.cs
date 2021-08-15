namespace DarkBattle.Services.ServiceModels.Merchants
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Consumables;
    using DarkBattle.Services.ServiceModels.Items;

    public class MerchantChampionViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<ItemViewServiceModel> Items { get; set; }

        public ICollection<ConsumableViewServiceModel> Consumables { get; set; }
    }
}
