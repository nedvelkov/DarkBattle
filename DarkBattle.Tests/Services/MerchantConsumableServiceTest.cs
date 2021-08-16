namespace DarkBattle.Tests.Services
{
    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;

    public class MerchantConsumableServiceTest
    {
        [Theory]
        [InlineData(50,50)]
        [InlineData(50,25)]
        public void TestSellConsumableToChampion(int championGold,int value)
        {
            //Arrange
            const string consumableName = "apple";
            const string consumabelId = "a1";
            const string championName = "S.Jobs";
            const string championId = "SJ";

            using var data = DatabaseMock.Instance;

            var champion = new Champion
            {
                Id = championId,
                Name = championName,
                Gold = championGold
            };
            var consumable = new Consumable
            {
                Id = consumabelId,
                Name = consumableName,
                Value = value
            };

            data.Champions.Add(champion);
            data.Consumables.Add(consumable);
            data.SaveChanges();

            var merchantConsumablesService = new MerchantConsumablesService(data);

            //Act
            var result = merchantConsumablesService.SellConsumable(championId, consumabelId);

            //Assert

            Assert.True(result);
        }
    }
}
