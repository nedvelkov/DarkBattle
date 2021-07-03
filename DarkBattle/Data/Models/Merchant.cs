namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Merchant
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; init; }

        [Required]
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<Consumable> Consumables { get; set; } = new List<Consumable>();
    }
}
