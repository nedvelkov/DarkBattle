namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class Area
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int MinLevelEnterence { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int MaxLevelCreatures { get; set; }

        public ICollection<Creature> Creatures { get; set; } = new List<Creature>();

    }
}
