namespace DarkBattle.Services.Models
{
    using System.Linq;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;

    using static DataConstants.Constants;

    public class BankService : IBankService
    {
        private readonly ApplicationDbContext data;

        public BankService(ApplicationDbContext data) 
            => this.data = data;

        public void GetLoan(string championId)
        {
            var champion = this.data.Champions.FirstOrDefault(x => x.Id == championId);

            champion.Gold += Loan;
            this.data.SaveChanges();
        }
    }
}
