namespace DarkBattle.ViewModels.Champions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DarkBattle.ViewModels.ChampionClasses;

    using static DataConstants.Constants;

    public class ChampionViewModel
    {
        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        public ICollection<ChampionClassPresentationModel> ChampionClasses { get; init; }

        public string ChampionClassId { get; set; }
    }
}
