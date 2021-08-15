namespace DarkBattle.ViewModels.CreatureConsumables
{
    using DarkBattle.Services.ServiceModels.Consumables;
    using System.Collections.Generic;

   public class CreatureConsumableViewModel
    {
        public string CreatureId { get; init; }
        public string CreatureName { get; init; }

        public ICollection<ConsumableViewServiceModel> Consumables { get; set; }

    }
}
