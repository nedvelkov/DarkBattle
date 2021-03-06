namespace DarkBattle.ViewModels.Merchants
{
    using DarkBattle.ViewModels.MerchantConsumables;
    using DarkBattle.Services.ServiceModels.Champions;

    public class SellConsumablesViewModel
    {
        private int currentPage = 1;
        private int maxPages;
        private int maxConsumablesPerPage = 5;
        public ChampionBarServiceModel Champion { get; init; }
        public MerchantConsumablePageModel Consumables { get; init; }


        public int MaxConsumablesPerPage => this.maxConsumablesPerPage;

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
                maxPages = value / MaxConsumablesPerPage + value % MaxConsumablesPerPage;
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
