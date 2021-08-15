namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Creatures;

    public interface ICreatureService
    {
        public void Add(CreatureServiceModel model);
        public void Edit(CreatureServiceModel model);
        public CreatureServiceModel GetCreature(string id);
        public ICollection<CreatureServiceListModel> CreaturesCollection();

        public ICollection<CreateureInAreaServiceModel> CreatureWithNoArea(int minLevel,int maxLevel);
        public ICollection<CreateureInAreaServiceModel> CreatureInArea(string areaId);

        public bool Delete(string id);

        public string CreatureName(string creatureId);

    }
}
