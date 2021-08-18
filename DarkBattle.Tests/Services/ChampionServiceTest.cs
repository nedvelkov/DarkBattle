namespace DarkBattle.Tests.Services
{
    using System.Linq;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Champions;

    public class ChampionServiceTest
    {

        [Fact]
        public void TestCreateChampion()
        {
            //Arrange
            const string championName = "zelda";
            const string championClassId = "shaman";
            const string playerId = "zzTop";

            var mapper = MapperMock.Instance;
            var config = ConfigurationMock.Configuration;

            using var data = DatabaseMock.Instance;

            var @class = new ChampionClass { Id = championClassId };
            var player = new Player { Id = playerId };
            data.ChampionClasses.Add(@class);
            data.Users.Add(player);
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            //Act
            var result= championService.CreateChampion(championName, championClassId, playerId);
            var test1 = data.Champions.First();

            //Assert
            Assert.True(result);

            Assert.True(test1.Name == championName);
            Assert.True(test1.ChampionClassId == championClassId);
            Assert.True(test1.PlayerId == playerId);

        }

        [Fact]
        public void TestChampionCollectionOfPlayer()
        {
            //Arrange
            const string championName = "zelda";
            const int championNum = 3;
            const string playerId = "zzTop";

            var mapper = MapperMock.Instance;
            var config = ConfigurationMock.Configuration;

            using var data = DatabaseMock.Instance;
            var player = new Player { Id = playerId };
            for (int i = 0; i < championNum; i++)
            {
                var champion = new Champion { Name = $"{championName}{i}" };
                player.Champions.Add(champion);
            }
            data.Users.Add(player);
            data.SaveChanges();

            var championService = new ChampionService(config, data,mapper);
            //Act
            var result = championService.ChampionCollection(playerId);
            var test1 = data.Champions.ToList();
            var test2 = result.First();

            //Assert
            Assert.True(result.Count == championNum);
            Assert.False(result.Any(x=>x.Name.Contains("3")));
            Assert.True(test1[2].Name == $"{championName}2");
            Assert.IsType<ChampionServiceModel>(test2);

        }

        [Fact]
        public void TestEquipItem()
        {
            //Arrange
            const string championId = "d2";
            const string championClassId = "shaman";
            const string championClassName = "shaman";
            const string itemId = "r2";
            const string gearId = "g2";

            var config = ConfigurationMock.Configuration;
            var mapper = MapperMock.Instance;

            using var data = DatabaseMock.Instance;

            var item = new Item { Id = itemId ,ObtainBy= championClassName };
            var championClass = new ChampionClass { Id = championClassId,Name= championClassName };
            var gear = new Gear { Id = gearId};
            var champion = new Champion { Id = championId,ChampionClass= championClass,Gear= gear };
            champion.Items.Add(item);
            data.Champions.Add(champion);
            data.Gears.Add(gear);
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);

            //Act
            var result = championService.EquipItem(championId, itemId);
            var test1 = data.Items.First();
            var test2 = data.Champions
                            .Include(x => x.Gear)
                            .ThenInclude(x => x.Items)
                            .First();

            //Assert
            Assert.True(result);
            Assert.True(test1.Id == itemId);
            Assert.True(test1.Equipped);
            Assert.True(test2.Gear.Items.Contains(item));
        }

        [Fact]
        public void TestSellItem()
        {
            //Arrange
            const string championId = "d2";
            const string championClassId = "shaman";
            const string championClassName = "shaman";
            const string itemId = "r2";
            const string gearId = "g2";

            var config = ConfigurationMock.Configuration;
            var mapper = MapperMock.Instance;

            using var data = DatabaseMock.Instance;

            var item = new Item { Id = itemId, ObtainBy = championClassName };
            var championClass = new ChampionClass { Id = championClassId, Name = championClassName };
            var gear = new Gear { Id = gearId };
            var champion = new Champion { Id = championId, ChampionClass = championClass, Gear = gear };
            champion.Items.Add(item);
            data.Champions.Add(champion);
            data.Gears.Add(gear);
            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);

            //Act
            var result = championService.SellItem(championId, itemId);
            var test1 = data.Items
                             .Include(x => x.Champions)
                            .First();
            var test2 = data.Champions
                            .Include(x => x.Gear)
                            .ThenInclude(x => x.Items)
                            .First();

            //Assert
            Assert.True(result);
            Assert.True(test1.Champions.Count == 0);
            Assert.False(test2.Gear.Items.Contains(item));
        }

        [Fact]
        public void TestDetailsChampion()
        {
            //Arrange
            const string championId = "d2";
            const string championClassId = "shaman";
            const string championClassName = "shaman";
            const string playerId = "g2";

            var config = ConfigurationMock.Configuration;
            var mapper = MapperMock.Instance;

            using var data = DatabaseMock.Instance;

            var championClass = new ChampionClass { Id = championClassId, Name = championClassName };
            var champion = new Champion { Id = championId, ChampionClass = championClass, };
            var player = new Player { Id = playerId };
            player.Champions.Add(champion);

            data.Users.Add(player);

            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);

            //Act
            var result = championService.Details(championId,playerId);
            var test1 = data.Champions.First();

            //Assert
            Assert.IsType<ChampionDetailServiceModel>(result);
            Assert.True(test1.ChampionClass.Name== championClassName);

        }
    }
}