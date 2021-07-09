namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ChampionClass
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString(); // int!!!

        [Required]
        public string Name { get; set; }

        [Required]
        public int Strenght { get; set; }

        [Required]
        public int Agility { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int SpellPower { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public ICollection<Champion> Champions { get; set; } = new List<Champion>();
    }
}
