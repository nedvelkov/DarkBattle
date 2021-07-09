namespace DarkBattle.Services
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.ChampionClasses;
    public interface IChampionClassService
    {
        public void Add(ChampionClassViewModel model);

        public void Edit(ChampionClassViewModel model);

        public ChampionClassViewModel GetClass(string id);

        public ICollection<ChampionClassListViewModel> ClassCollection();

        public bool Delete(string id);
    }
}
