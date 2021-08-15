namespace DarkBattle.Services.Interface
{
    public interface ICreatureConsumablesService
    {
        public bool Add(string consumableId, string creatureId);
        public bool Remove(string consumableId, string creatureId);
    }
}
