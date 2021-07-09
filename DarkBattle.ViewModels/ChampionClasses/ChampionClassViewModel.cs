namespace DarkBattle.ViewModels.ChampionClasses
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class ChampionClassViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [Range(MinValue, int.MaxValue)]
        public int Strenght { get; set; }

        [Required]
        [Range(MinValue, int.MaxValue)]
        public int Agility { get; set; }

        [Required]
        [Range(MinValue, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        [Range(MinValue, int.MaxValue)]
        public int SpellPower { get; set; }

        [Required]
        [DisplayName("Image")]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }

    }
}
