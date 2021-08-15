namespace DarkBattle.Services.Interface
{
    
   public interface IAreaCreatureService
    {
        public void Add(string creatureId, string areaId);

        public void Remove(string creatureId, string areaId);

    }
}
