namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class ChampionClass
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Strenght { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Agility { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Health { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int SpellPower { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public ICollection<Champion> Champions { get; set; } = new List<Champion>();
    }
}
