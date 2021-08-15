namespace DarkBattle.Services.ServiceModels.Areas
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Champions;

   public class BattleZoneViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<AreaServiceViewModel> Areas { get; init; }
    }
}
