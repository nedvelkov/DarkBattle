namespace DarkBattle.Services.Interface
{
    public interface ICreatureItemsService
    {
        public bool Add(string itemId, string creatureId);

        public bool Remove(string itemId, string creatureId);
    }
}
