namespace DarkBattle.Services.Interface
{

    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Items;

    public interface IItemService
    {
        public void Add(ItemServiceModel model);

        public void Edit(ItemServiceModel model);

        public ItemServiceModel GetItem(string id);

        public ICollection<ItemServiceListModel> ItemsCollection();

        public ICollection<ItemServiceListModel> CreatureItems(string creatureId);
        public ICollection<ItemServiceListModel> ItemsWithNoCreature();

        public ICollection<ItemServiceListModel> ItemsWithNoMerchant();
        public ICollection<ItemServiceModel> ItemsSellByMerchant(string merchantId);




        public bool Delete(string id);
    }
}
