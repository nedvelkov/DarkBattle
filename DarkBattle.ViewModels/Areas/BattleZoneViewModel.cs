namespace DarkBattle.ViewModels.Areas
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Areas;
    using DarkBattle.Services.ServiceModels.Champions;

   public class BattleZoneViewModel
    {
        private int currentPage = 1;
        public ChampionBarServiceModel Champion { get; init; }
        public ICollection<AreaServiceViewModel> Areas { get; set; }

        public int MaxAreasPerPage => 1;

        public int MaxPages { get; init; }

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
