namespace DarkBattle.Services.Interface
{

   public interface IMerchantItemsService
    {
        public void Add(string itemId, string merchantId);

        public void Remove(string itemId, string merchantId);

        public bool SellItem(string championId, string itemId);

    }
}
