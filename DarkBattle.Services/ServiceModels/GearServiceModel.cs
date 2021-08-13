namespace DarkBattle.Services.ServiceModels
{
    using System.Collections.Generic;

    public class GearServiceModel
    {
        public string GearId { get; set; }

        public string ChampionId { get; set; }

        public ICollection<ItemViewServiceModel> EquipedItems { get; set; } = new List<ItemViewServiceModel>();
    }
}
