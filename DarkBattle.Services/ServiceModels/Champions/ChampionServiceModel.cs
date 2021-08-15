namespace DarkBattle.Services.ServiceModels.Champions
{
   public class ChampionServiceModel
    {
        public string ChampionId { get; init; }
        public string ChampionClassId { get; init; }

        public string Name { get; init; }

        public string ChampionClass { get; init; }

        public string ImageUrl { get; init; }

        public int Level { get; set; }
        public int Experience { get; set; }

    }
}
