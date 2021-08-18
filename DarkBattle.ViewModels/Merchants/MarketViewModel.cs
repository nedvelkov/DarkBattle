namespace DarkBattle.ViewModels.Merchants
{

    using DarkBattle.Services.ServiceModels.Merchants;
    public class MarketViewModel : MerchantMarketViewModel
    {
        private int currentPage = 1;
        private int maxPages;
        private int maxMerchantsPerPage = 3;

        public int MaxMerchantsPerPage => this.maxMerchantsPerPage;

        public int MaxPages
        {
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
                maxPages = value / MaxMerchantsPerPage + value % MaxMerchantsPerPage;
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
