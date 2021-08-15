namespace DarkBattle.ViewModels.Champions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;  

    using static DataConstants.Constants;

    public class ChampionViewModel
    {
        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

     //   public ICollection<ChampionClassServiceListToChampionModel> ChampionClasses { get; init; }

        public string ChampionClassId { get; set; }
    }
}
