using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBattle.Data.Models
{
    public class Creature
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int Attack { get; set; }

        [Required]
        public int Defense { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int Block { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Gold { get; set; }

        [Required]
        public int Expirience { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public string AreaId { get; set; }
        public Area Area { get; set; }

    }
}
