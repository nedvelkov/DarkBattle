namespace DarkBattle.Services.ServiceModels.Creatures
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    using static DataConstants.Constants;

    public class CreatureServiceModel
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
        [Range(MinValue, MaxAttackCreature)]
        public int Attack { get; set; }

        [Required]
        [Range(MinValue, MaxDefenseCreature)]
        public int Defense { get; set; }

        [Required]
        [Range(MinValue, MaxHealthCreature)]
        public int Health { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Block { get; set; }

        [Required]
        [Range(MinValue, MaxLevelCreature)]
        public int Level { get; set; }

        [Required]
        [Range(MinValue, MaxGoldDrop)]
        public int Gold { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Expirience { get; set; }
    }
}
