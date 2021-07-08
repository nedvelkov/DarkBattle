namespace DarkBattle.ViewModels.Consumables
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    using static DataConstants.Constants;

    public class ConsumableViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Image")]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinValue, MaxHealthRestore)]
        public int RestoreHealth { get; init; }

        [Required]
        [Range(MinValue, int.MaxValue)]
        public int Value { get; set; }
    }
}
