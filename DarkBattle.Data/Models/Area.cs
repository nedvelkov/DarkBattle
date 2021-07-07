namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Area
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int MinLevelEnterence { get; set; }

        [Required]
        public int MaxLevelCreatures { get; set; }

        public ICollection<Creature> Creatures { get; set; } = new List<Creature>();

    }
}
