namespace DarkBattle.Services.Interface
{
    public interface IMerchantConsumablesService
    {
        public void Add(string consumableId, string merchantId);

        public void Remove(string consumableId, string merchantId);
        public bool SellConsumable(string championId, string itemId);
    }
}
