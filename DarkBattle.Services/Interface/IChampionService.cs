namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;

    using DarkBattle.Services.ServiceModels.Champions;
    using DarkBattle.Services.ServiceModels.ChampionClass;

    public interface IChampionService
    {
        public ICollection<ChampionClassServiceListToChampionModel> GetChampionClasses();
        public bool CreateChampion(string name, string championClassId, string playerId);

        public ICollection<ChampionServiceModel> ChampionCollection(string playerId);
        public bool DeleteChampion(string championId, string playerId);
        public ChampionDetailServiceModel Details(string championId, string playerId);
        public ChampionBarServiceModel ChampionBar(string championId, string playerId);

        public bool EquipItem(string championId, string itemId);

        public bool RemoveItem(string championId, string itemId);

        public bool SellItem(string championId, string itemId);

        public bool Consume(string championId, string consumableId);

    }
}
