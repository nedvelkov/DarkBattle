namespace DarkBattle.ViewModels.Items
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Items;

    public class ItemViewModel : ItemServiceModel
    {

        public ICollection<string> ChampionClasses { get; set; }

        public ICollection<string> ItemType => new List<string> { "Helm", "Weapon", "Chestplate", "Shield", "Gloves", "Pants", "Boots" };
    }
}
