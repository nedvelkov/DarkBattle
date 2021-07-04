namespace DarkBattle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static DataConstants.Constants;
    public class CreateCreatureViewModel
    {
        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string Image { get; set; }

        [Range(MinValue,MaxLevelCreature)]
        public int Attack { get; set; }

        [Range(MinValue, MaxLevelCreature)]
        public int Defense { get; set; }

        [Range(MinValue, MaxLevelCreature)]
        public int Health { get; set; }

        [Range(MinValue, MaxLevelCreature)]
        public int Block { get; set; }

        [Range(MinValue, MaxLevelCreature)]
        public int Level { get; set; }

        [Range(MinValue, MaxLevelCreature)]
        public int Gold { get; set; }

        [Range(MinValue,MaxLevelCreature)]
        public int Expirience { get; set; }

    }
}