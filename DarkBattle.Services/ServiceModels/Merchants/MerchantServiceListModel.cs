namespace DarkBattle.Services.ServiceModels.Merchants
{

    using System.ComponentModel.DataAnnotations;

    public class MerchantServiceListModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sell items")]
        public int ItemCount { get; set; }

        [Display(Name = "Sell consumables")]
        public int ConsumableCount { get; set; }
    }
}
