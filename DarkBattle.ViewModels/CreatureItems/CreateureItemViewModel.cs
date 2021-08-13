namespace DarkBattle.ViewModels.CreatureItems
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Items;

   public class CreateureItemViewModel
    {
        public string CreatureId { get; init; }
        public string CreatureName { get; init; }

        public ICollection<ItemsListView> ItemCollection { get; set; }

    }
}
