namespace DarkBattle.Services.ServiceModels
{
    using System.Collections.Generic;

    public class AreaServiceModel
    {
        public string Id { get; init; } 

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public int MinLevelEnterence { get; init; }

        public int MaxLevelCreatures { get; init; }

        public ICollection<CreatureServiceModel> Creatures { get; set; }
    }
}
