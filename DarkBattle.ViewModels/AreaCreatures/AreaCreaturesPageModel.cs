namespace DarkBattle.ViewModels.AreaCreatures
{
    using System.ComponentModel.DataAnnotations;

    using DarkBattle.ViewModels.Enums;
    public class AreaCreaturesPageModel : AreaCreatureAddViewModel
    {

        [Display(Name = "Search by name")]
        public string SearchTerm { get; init; }

        public CreatureSorting Sorting { get; init; }

        [Display(Name = "Search by level")]
        public int? CurrentLevel { get; init; }

    }
}
