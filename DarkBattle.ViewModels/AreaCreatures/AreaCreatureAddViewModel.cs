namespace DarkBattle.ViewModels.AreaCreatures
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Areas;
    using DarkBattle.Services.ServiceModels.Creatures;

    public class AreaCreatureAddViewModel
    {
        public AreaServiceListModel Area { get; set; }

        public ICollection<CreateureInAreaServiceModel> Creatures { get; set; }

    }
}
