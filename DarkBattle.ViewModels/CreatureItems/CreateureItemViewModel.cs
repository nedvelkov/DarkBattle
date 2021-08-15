namespace DarkBattle.ViewModels.CreatureItems
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Items;

   public class CreateureItemViewModel
    {
        public string CreatureId { get; init; }
        public string CreatureName { get; init; }

       public ICollection<ItemServiceListModel> ItemCollection { get; set; }

    }
}
