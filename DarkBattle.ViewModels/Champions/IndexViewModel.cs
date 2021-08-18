namespace DarkBattle.ViewModels.Champions
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Champions;

    public class IndexViewModel
    {
        public bool IsBanned { get; init; }
        public IEnumerable<ChampionServiceModel> Champions { get; set; }
    }
}
