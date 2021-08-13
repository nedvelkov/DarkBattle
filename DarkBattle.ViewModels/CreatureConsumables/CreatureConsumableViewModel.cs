namespace DarkBattle.ViewModels.CreatureConsumables
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Consumables;
   public class CreatureConsumableViewModel
    {
        public string CreatureId { get; init; }
        public string CreatureName { get; init; }

        public ICollection<ConsumableViewModel> Consumables { get; set; }

    }
}
