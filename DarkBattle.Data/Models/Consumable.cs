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
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int RestoreHealth { get; set; }

        [Required]
        public int Value { get; set; }

        public ICollection<Champion> Champions { get; set; } = new List<Champion>();
        public string CreatureId { get; set; }
        public Creature Creature { get; set; }
        public string MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
