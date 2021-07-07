namespace DarkBattle.Services
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Creatures;

    public interface ICreatureService
    {
        public void AddCreature(CreatureViewModel model);
        public CreatureViewModel GetCreature(string id);
        public bool Delete(string id);
        public void EditCreature(CreatureViewModel model);
        public ICollection<CreatureListViewModel> CreatureCollection();

    }
}
