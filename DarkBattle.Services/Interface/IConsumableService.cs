namespace DarkBattle.Services.Interface
{
    using System.Collections.Generic;
    using DarkBattle.Services.ServiceModels.Consumables;

  public  interface IConsumableService
    {
        public void Add(ConsumableViewServiceModel model);

        public void Edit(ConsumableViewServiceModel model);

        public ConsumableViewServiceModel GetConsumable(string id);

        public ICollection<ConsumableViewServiceModel> ConsumablesCollection();
        public ICollection<ConsumableViewServiceModel> ConsumablesWithNoMerchant();
        public ICollection<ConsumableViewServiceModel> ConsumablesSellByMerchant(string merchantId);

        public ICollection<ConsumableViewServiceModel> ConsumablesWithNoCreature();
        public ICollection<ConsumableViewServiceModel> ConsumablesOwnByCreature(string creatureId);


        public bool Delete(string id);
    }
}
