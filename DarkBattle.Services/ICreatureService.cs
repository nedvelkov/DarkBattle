namespace DarkBattle.Services
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Creatures;

    public interface ICreatureService
    {
        public void Add(CreatureViewModel model);
        public void Edit(CreatureViewModel model);
        public CreatureViewModel GetCreature(string id);
        public ICollection<CreatureListViewModel> CreaturesCollection();
        public bool Delete(string id);

    }
}
