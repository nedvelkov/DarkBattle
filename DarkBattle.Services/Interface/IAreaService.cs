namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Areas;

    public interface IAreaService
    {
        public void Add(AreaServiceViewModel model);

        public void Edit(AreaServiceViewModel model);
        
        public AreaServiceViewModel GetArea(string id);

        public AreaServiceListModel AreaForCreatures (string areaId);

        public ICollection<AreaServiceListModelExtention> AreasCollection();

        public bool Delete(string id);

        public ICollection<AreaServiceViewModel> AreaServiceCollection();
    }
}
