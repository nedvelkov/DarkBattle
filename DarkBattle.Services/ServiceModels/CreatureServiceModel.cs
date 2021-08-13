namespace DarkBattle.Services.ServiceModels
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Consumables;

    public class CreatureServiceModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Health { get; set; }

        public int Block { get; set; }

        public int Level { get; set; }

        public int Gold { get; set; }

        public int Expirience { get; set; }
    }
}
