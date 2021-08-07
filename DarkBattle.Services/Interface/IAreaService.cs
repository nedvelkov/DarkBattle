namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels;
    using DarkBattle.ViewModels.Areas;

    public interface IAreaService
    {
        public void Add(AreaViewModel model);

        public void Edit(AreaViewModel model);
        
        public AreaViewModel GetArea(string id);

        public ICollection<AreasListViewModel> AreasCollection();

        public bool Delete(string id);

        public ICollection<AreaServiceViewModel> AreaServiceCollection();
    }
}
