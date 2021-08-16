namespace DarkBattle.ViewModels.Merchants
{

    using DarkBattle.Services.ServiceModels.Champions;
    using DarkBattle.ViewModels.MerchantItems;
    public class SellItemsViewModel
    {
        private int currentPage = 1;
        private int maxPages;
        private int maxItemsPerPage = 5;
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantItemPageModel Items { get; init; }

        public int MaxItemsPerPage => this.maxItemsPerPage;

        public int MaxPages {
            get
            {
                return this.maxPages;
            }
                init
            {
                if (value == 0)
                {
                    maxPages = 1;
                }
                maxPages = value / MaxItemsPerPage + value % MaxItemsPerPage;
            }
        }

        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }
            set
            {
                if (value <= 0)
                {
                    this.currentPage = 1;
                }
                else
                {
                    this.currentPage = value;
                }
            }
        }

        public int PreviusPageNumber => this.CurrentPage - 1;
        public int NextPageNumber => this.CurrentPage + 1;
    }
}
