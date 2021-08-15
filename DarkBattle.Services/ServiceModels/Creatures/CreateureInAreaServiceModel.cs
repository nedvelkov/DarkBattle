namespace DarkBattle.Services.ServiceModels.Creatures
{
   public class CreateureInAreaServiceModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public int Level { get; init; }

        public int Expirience { get; init; }

        public string ImageUrl { get; set; }

        public int Attack { get; init; }
    }
}
