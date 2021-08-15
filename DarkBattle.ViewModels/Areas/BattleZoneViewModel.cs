namespace DarkBattle.ViewModels.Areas
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Areas;
    using DarkBattle.Services.ServiceModels.Champions;

   public class BattleZoneViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<AreaServiceViewModel> Areas { get; init; }
    }
}
