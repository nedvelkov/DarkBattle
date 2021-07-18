namespace DarkBattle.Services.Interface
{
    using DarkBattle.ViewModels.AreaCreatures;
    
   public interface IAreaCreatureService
    {
        public void Add(string creatureId, string areaId);

        public void Remove(string creatureId, string areaId);

        public AreaCreatureAddViewModel ListAllAvilableCreatures(string areaId);

        public AreaCreaturesPageModel ListAllCreaturesInArea(string areaId);

        public AreaCreaturesPageModel SortCreatures(AreaCreaturesPageModel model);

    }
}
