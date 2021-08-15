namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.ChampionClass;
    public interface IChampionClassService
    {
        public void Add(ChampionClassServiceModel model);

        public void Edit(ChampionClassServiceModel model);

        public ChampionClassServiceModel GetClass(string id);

        public ICollection<ChampionClassServiceListModel> ClassCollection();

        public ICollection<string> ChampionClassCollection();

        public bool Delete(string id);
    }
}
