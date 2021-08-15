namespace DarkBattle.Services.ServiceModels
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Champions;

    public class BattleViewModel
    {
        public ChampionBarServiceModel Champion { get; set; }
        
        public string ChampionImg { get; init; }
        
        public string CreatureName { get; init; }
        
        public string CreatureImg { get; init; }
        
        public string AreaName { get; init; }
        
        public string AreaImg { get; init; }

        public string AreaId { get; init; }

        public List<string> BattleLog { get; set; }

    }
}
