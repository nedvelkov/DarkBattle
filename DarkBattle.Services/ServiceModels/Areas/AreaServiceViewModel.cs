namespace DarkBattle.Services.ServiceModels.Areas
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class AreaServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Image")]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }

        [Required]
        [DisplayName("Minimum level entrence")]
        [Range(MinValue, MaxLevelCreature)]
        public int MinLevelEnterence { get; set; }

        [Required]
        [DisplayName("Max level creatures")]
        [Range(MinValue, MaxLevelCreature)]
        public int MaxLevelCreatures { get; set; }
    }
}
