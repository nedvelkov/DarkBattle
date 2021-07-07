namespace DarkBattle.Services
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Areas;

    public interface IAreaService
    {
        public void AddAreas(AreaViewModel model);

        public void EditArea(AreaViewModel model);
        
        public AreaViewModel GetArea(string id);

        public ICollection<AreasListViewModel> AreasCollection();

        public bool DeleteArea(string id);  
    }
}
