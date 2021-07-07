﻿
namespace DarkBattle.ViewModels.Items
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class ItemViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [DisplayName("Image")]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]

        public int Attack { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]

        public int Defense { get; set; }

        [Required]
        [DisplayName("Obtain by")]
        public string ObtainBy { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        [DisplayName("Required level")]
        public int RequiredLevel { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int Value { get; set; }
    }
}
