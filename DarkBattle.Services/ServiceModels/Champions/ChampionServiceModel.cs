namespace DarkBattle.Services.ServiceModels.Champions
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class ChampionServiceModel
    {
        public string ChampionId { get; init; }
        public string ChampionClassId { get; init; }
        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; init; }

        public string ChampionClass { get; init; }
        [Required]
        [DisplayName("Image")]
        public string ImageUrl { get; init; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Level { get; set; }

        [Required]
        [Range(MinValue, MaxValue)]
        public int Experience { get; set; }

    }
}
