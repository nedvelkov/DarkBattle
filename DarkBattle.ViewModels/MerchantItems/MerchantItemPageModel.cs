namespace DarkBattle.ViewModels.MerchantItems
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DarkBattle.ViewModels.Items;

    public class MerchantItemPageModel
    {
        public string MerchantId { get; init; }
        public string MerchantName { get; init; }
        public ICollection<ItemViewModel> ItemCollection { get; set; }

        public ICollection<string> ItemsType
        {
            get
            {
                if (IsInitilize())
                {
                    return this.ItemCollection.Select(x => x.Type)
                                                .Distinct()
                                                .ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Item type")]
        public string SelectItemType { get; set; }
        public ICollection<int> ItemLevel
        {
            get
            {
                if (IsInitilize())
                {
                    return this.ItemCollection.Select(x => x.RequiredLevel)
                                                 .Distinct()
                                                 .ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Item level")]
        public int SelectItemLevel { get; set; }


        public ICollection<string> ObtainBy
        {
            get
            {
                if (IsInitilize())
                {
                    return this.ItemCollection.Select(x => x.ObtainBy)
                                         .Distinct()
                                         .ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        [Display(Name = "Obtain by")]
        public string SelectObteinBy { get; set; }

        private bool IsInitilize()
        => this.ItemCollection != null;
    }
}
