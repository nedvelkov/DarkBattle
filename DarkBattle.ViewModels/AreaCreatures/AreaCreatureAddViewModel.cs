namespace DarkBattle.ViewModels.AreaCreatures
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Areas;
    using DarkBattle.ViewModels.Creatures;

    public class AreaCreatureAddViewModel
    {
        public AreaCreatureViewModel Area { get; set; }

        public ICollection<CreatureAreaViewModel> Creatures { get; set; }

    }
}
