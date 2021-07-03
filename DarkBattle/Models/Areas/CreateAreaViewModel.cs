namespace DarkBattle.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static DataConstants.Constants;

    public class CreateAreaViewModel
    {

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinValue,MaxLevelCreature)]
        public int MinLevelEnterence { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int MaxLevelCreatures { get; set; }
    }
}