namespace DarkBattle.Services.Interface
{
   public interface IPlayerService
    {
        public void LogIn(string email);
        public void LogOut(string playerId);
    }
}
