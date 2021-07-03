namespace DarkBattle.Data.Models
{
using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Consumable
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int RestoreHealth { get; init; }

        [Required]
        public int Value { get; set; }

        public ICollection<Champion> ChampionConsumables { get; set; } = new List<Champion>();
    }
}
