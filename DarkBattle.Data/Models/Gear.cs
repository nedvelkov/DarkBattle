namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Gear
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public string ChampionId { get; init; }
        public Champion Champion { get; init; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

    }
}
