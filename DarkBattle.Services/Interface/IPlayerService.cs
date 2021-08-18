namespace DarkBattle.Services.Interface
{
    using DarkBattle.Services.ServiceModels.Home;
    using System;

    public interface IPlayerService
    {
        public void LogIn(string email);
        public void LogOut(string playerId);
        public AdminServiceControl AdminView();
        public void BanPlayer( string playerId);
        public void RemoveBan( string playerId);

        public bool IsBanned(string playerId);
    }
}
