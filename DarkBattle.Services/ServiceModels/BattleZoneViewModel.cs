namespace DarkBattle.Services.ServiceModels
{
    using System.Collections.Generic;

   public class BattleZoneViewModel
    {
        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<AreaServiceViewModel> Areas { get; init; }
    }
}
