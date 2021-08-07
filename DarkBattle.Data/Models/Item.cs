
namespace DarkBattle.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class Item
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Defense { get; set; }

        [Required]
        public string ObtainBy { get; set; }

        [Required]
        public int RequiredLevel { get; set; }

        [Required]
        public int Value { get; set; }

        public bool Equipped { get; set; }

        public ICollection<Champion> Champions { get; set; } = new List<Champion>();

        public string CreatureId { get; set; }
        public Creature Creature { get; set; }
        public string MerchantId { get; set; }
        public Merchant Merchant { get; set; }
    }
}
