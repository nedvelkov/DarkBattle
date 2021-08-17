namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class Champion
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Experience { get; set; }

        [Required]
        [Range(MinValue, MaxChampionLevel)]
        public int Level { get; set; }

        [Required]
        [Range(MinGold, MaxValue)]
        public int Gold { get; set; }

        [Required]
        public string ChampionClassId { get; set; }

        [Required]
        public ChampionClass ChampionClass { get; set; }

        [Range(MinValue, MaxValue)]
        public int CurrentHealth { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ICollection<Consumable> ChampionConsumables { get; set; } = new List<Consumable>();

        public string PlayerId { get; init; }

        public Player Player { get; init; }

        public string GearId { get; init; }
        public Gear Gear { get; set; }
    }
}
