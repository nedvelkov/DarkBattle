namespace DarkBattle.Services.Models
{
    using System.Linq;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;

    public class PlayerService : IPlayerService
    {

        private readonly ApplicationDbContext data;

        public PlayerService(ApplicationDbContext data) 
            => this.data = data;

        public void LogIn(string email)
        {
            var player = this.data.Users
                          .Single(x => x.Email == email);
            player.IsOnline = true;

            this.data.SaveChanges();
        }


        public void LogOut(string playerId)
        {
            var player = this.data.Users
                          .Single(x => x.Id == playerId);
            player.IsOnline = false;
            this.data.SaveChanges();
        }
    }
}
