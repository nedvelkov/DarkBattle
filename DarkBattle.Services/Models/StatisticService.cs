namespace DarkBattle.Services.Models
{
    using System.Linq;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;


    public class StatisticService: IStatisticService
    {
        private readonly ApplicationDbContext data;

        public StatisticService(ApplicationDbContext data) 
            => this.data = data;

        public int TotalAreas()
            => this.data.Areas.Count();

        public int TotalChampionClass()
            => this.data.ChampionClasses.Count();


        public int TotalOnlinePlayers()
            => this.data.Users.Where(x => x.IsOnline).Count();


        public int TotalPlayers()
            => this.data.Users.Count();
    }
}
