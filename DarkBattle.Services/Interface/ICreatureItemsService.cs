namespace DarkBattle.Services.Interface
{
    using DarkBattle.ViewModels.CreatureItems;

    public interface ICreatureItemsService
    {
        public CreateureItemViewModel AddItems(string creatureId);
        public CreateureItemViewModel Items(string creatureId);

        public bool Add(string itemId, string creatureId);
        public bool Remove(string itemId, string creatureId);
    }
}
