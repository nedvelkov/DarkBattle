namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Champion
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public int Expirience { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Gold { get; set; }

        [Required]
        public int Expirence { get; set; }

        [Required]
        public string ChampionClassId { get; set; }

        [Required]
        public ChampionClass ChampionClass { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<Consumable> ChampionConsumables { get; set; } = new List<Consumable>();

    }
}
