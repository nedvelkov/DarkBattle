namespace DarkBattle.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class Creature
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinValue, MaxAttackCreature)]
        public int Attack { get; set; }

        [Required]
        [Range(MinValue, MaxDefenseCreature)]
        public int Defense { get; set; }

        [Required]
        [Range(MinValue, MaxHealthCreature)]
        public int Health { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Block { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int Level { get; set; }

        [Required]
        [Range(MinValue, MaxGoldDrop)]
        public int Gold { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Expirience { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<Consumable> Consumables { get; set; } = new List<Consumable>();

        public string AreaId { get; set; }
        public Area Area { get; set; }

    }
}
