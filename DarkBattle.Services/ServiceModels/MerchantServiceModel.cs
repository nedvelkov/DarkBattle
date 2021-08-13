namespace DarkBattle.Services.ServiceModels
{

    using System.Collections.Generic;

    public class MerchantServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<ItemViewServiceModel> Items { get; set; }

        public ICollection<ConsumableViewServiceModel> Consumables { get; set; }
    }
}
