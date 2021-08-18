namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Items;
    using DarkBattle.Services.ServiceModels.Merchants;

    public class MerchantServiceTest
    {
        [Fact]
        public void TestGetMerchantById()
        {
            //Arrange
            const string merchantId = "TestId";

            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;
            var config = ConfigurationMock.Configuration;

            data.Merchants.Add(new Merchant { Id = merchantId });
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper,championService);

            //Act
            var result = merchantService.GetMerchant(merchantId);

            //Assert
            Assert.True(result.Id == merchantId);
            Assert.IsType<MerchantServiceModel>(result);
        }

        [Fact]
        public void TestGetMerchantCollection()
        {
            //Arrange
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;

            data.Merchants.AddRange(Enumerable.Range(0, 10).Select(x => new Merchant { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper, championService);

            //Act
            var result = merchantService.MerchantsCollection();

            //Assert
            Assert.IsType<List<MerchantServiceListModel>>(result);
            Assert.True(result.Count == 10);

        }

        [Fact]
        public void TestAddNewMerchant()
        {
            //Arrange
            const string name = "Test";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance; 
            var config = ConfigurationMock.Configuration;

            var model = new MerchantServiceModel() { Name = name };

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper, championService);

            //Act
            merchantService.Add(model);
            var result = data.Merchants.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Name == name);

        }

        [Fact]
        public void TestEditMerchant()
        {
            //Arrange
            const string name = "Test";
            const string description = "TestD";
            const string id = "id";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;


            data.Merchants.Add(new Merchant { Id = id, Name = name });
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper, championService);

            //Act
            var model = merchantService.GetMerchant(id);
            model.Description = description;
            merchantService.Edit(model);
            var result = data.Merchants.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Description == description);
        }

        [Fact]
        public void TestDeleteMerchant()
        {
            //Arrange
            const string id = "testId";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;

            data.Merchants.Add(new Merchant { Id = id });

            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper,championService);

            //Act
            var result = merchantService.Delete(id);
            var test2 = data.Merchants.FirstOrDefault();

            //Assert
            Assert.True(result);
            Assert.Null(test2);
        }

        [Fact]
        public void TestAllMerchants()
        {
            //Arrange
            const string championId = "AtestId";
            const string playerId = "BtestId";
            const string championClassId = "CtestId";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;

            data.Merchants.AddRange(Enumerable.Range(0, 10).Select(x => new Merchant { Id = Guid.NewGuid().ToString() }));
            var player = new Player { Id = playerId };
            var championClass = new ChampionClass { Id = "championClassId" };
            var champion = new Champion 
            {
                Id = championId,
                Level=1,
                Player=player,
                PlayerId=playerId,
                ChampionClass=championClass,
                ChampionClassId=championClassId
            };
            player.Champions.Add(champion);
            data.Users.Add(player);
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var merchantService = new MerchantService(data, mapper, championService);

            //Act
            var result = merchantService.AllMerchants(championId,playerId);

            //Assert
            Assert.True(result.Champion.ChampionId==championId);
            Assert.True(result.Merchants.Count == 10);
        }
    }
}
