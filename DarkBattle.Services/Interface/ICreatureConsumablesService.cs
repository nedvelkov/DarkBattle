namespace DarkBattle.Services.Interface
{
    using DarkBattle.ViewModels.CreatureConsumables;

    public interface ICreatureConsumablesService
    {
        public CreatureConsumableViewModel Items(string creatureId);
        public CreatureConsumableViewModel AddItems(string creatureId);

        public bool Add(string consumableId, string creatureId);
        public bool Remove(string consumableId, string creatureId);
    }
}
