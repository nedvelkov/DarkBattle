namespace DarkBattle.Services
{
    using System.Collections.Generic;

    using DarkBattle.ViewModels.Consumables;

  public  interface IConsumableService
    {
        public void Add(ConsumableViewModel model);

        public void Edit(ConsumableViewModel model);

        public ConsumableViewModel GetConsumable(string id);

        public ICollection<ConsumableListViewModel> ConsumablesCollection();

        public bool Delete(string id);
    }
}
