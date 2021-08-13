namespace DarkBattle.ViewModels.MerchantConsumables
{
    using DarkBattle.ViewModels.Enums;
    using System.ComponentModel.DataAnnotations;

    public class MerchantConsumablePageModel : MerchantConsumablesAddViewModel
    {
        [Display(Name = "Search by name")]
        public string SearchByName { get; set; }

        [Display(Name = "Sort by")]
        public ConsumableSorting Sorting { get; set; }

    }
}
