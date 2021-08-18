namespace DarkBattle.Services.ServiceModels.Home
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Champions;


    public class PlayerServiceModel
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public ICollection<ChampionServiceModel> Champions { get; set; }

        public bool IsBanned { get; set; }

    }
}
