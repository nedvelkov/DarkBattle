namespace DarkBattle.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Creature
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Defense { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int Block { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Gold { get; set; }

        [Required]
        public int Expirience { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<Consumable> Consumables { get; set; } = new List<Consumable>();

        public string AreaId { get; set; }
        public Area Area { get; set; }

    }
}
