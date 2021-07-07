namespace DarkBattle.Services
{

    using System.Collections.Generic;

    using DarkBattle.ViewModels.Items;

    public interface IItemService
    {
        public void AddItem(ItemViewModel model);

        public void EditItem(ItemViewModel model);

        public ItemViewModel GetItem(string id);

        public ICollection<ItemListViewModel> ItemCollection();

        public bool DeleteItem(string id);
    }
}
