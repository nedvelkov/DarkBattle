namespace DarkBattle.ViewModels.Creatures
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;
    public class CreatureViewModel
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