namespace DarkBattle.Services.ServiceModels
{
   public class ChampionBarServiceModel
    {
        public string ChampionId { get; init; }
        public string Name { get; init; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int BaseHealth { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceForLevelUp { get; set; }
        public int Gold { get; set; }
    }
}
