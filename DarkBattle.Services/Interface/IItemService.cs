namespace DarkBattle.Services.Interface
{

    using System.Collections.Generic;

    using DarkBattle.ViewModels.Items;

    public interface IItemService
    {
        public void Add(ItemViewModel model);

        public void Edit(ItemViewModel model);

        public ItemViewModel GetItem(string id);

        public ItemViewModel GetChamponClasses();

        public ICollection<ItemListViewModel> ItemsCollection();

        public bool Delete(string id);
    }
}
