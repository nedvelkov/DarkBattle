namespace DarkBattle.Services.ServiceModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DarkBattle.Services.ServiceModels.Champions;

    using static DataConstants.Constants;

    public class PlayerServiceModel
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public ICollection<ChampionServiceModel> Champions { get; set; }

        public bool IsBanned { get; set; }

        [Required]
        [MinLength(MinBanMassage)]
        public string BanMessage { get; set; }
    }
}
