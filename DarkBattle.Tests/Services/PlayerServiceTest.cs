namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Areas;
    using DarkBattle.Services.Interface;

    public class PlayerServiceTest
    {
        [Fact]
        public void TestSetPlayerOnilne()
        {
            //Arrange
            const string email = "www@aw.bg";
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new Player { Email = email });
            data.SaveChanges();

            var statisticService = new StatisticService(data);
            var playerService = new PlayerService(data, mapper, statisticService);
            //Act
            playerService.LogIn(email);
            var result = data.Users.First();
            //Assert
            Assert.True(result.IsOnline);
            Assert.True(result.Email == email);

        }

        [Fact]
        public void TestSetPlayerOffline()
        {
            //Arrange
            const string playerId = "player1123";
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new Player { Id = playerId,IsOnline=true });
            data.SaveChanges();

            var statisticService = new StatisticService(data);
            var playerService = new PlayerService(data, mapper, statisticService);
            //Act
            playerService.LogOut(playerId);
            var result = data.Users.First();
            //Assert
            Assert.False(result.IsOnline);

        }

        [Fact]
        public void TestPlayerBan()
        {
            //Arrange
            const string playerId = "player1123";
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new Player { Id = playerId });
            data.SaveChanges();

            var statisticService = new StatisticService(data);
            var playerService = new PlayerService(data, mapper, statisticService);
            //Act
            playerService.BanPlayer(playerId);
            var result = data.Users.First();
            //Assert
            Assert.True(result.IsBanned);

        }

        [Fact]
        public void TestPlayerRemoveBan()
        {
            //Arrange
            const string playerId = "player1123";
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Users.Add(new Player { Id = playerId ,IsBanned=true});
            data.SaveChanges();

            var statisticService = new StatisticService(data);
            var playerService = new PlayerService(data, mapper, statisticService);
            //Act
            playerService.RemoveBan(playerId);
            var result = data.Users.First();
            //Assert
            Assert.False(result.IsBanned);

        }

        [Fact]
        public void TestAdminViewGetsCorrectData()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var players = Enumerable.Range(0, 10).Select(x => new Player()).ToList();
            foreach (var player in players)
            {
                var champion = new Champion { Id = Guid.NewGuid().ToString()};
                player.Champions.Add(champion);
                player.IsOnline = true;
            }
            data.Users.AddRange(players);
            data.SaveChanges();

            var statisticService = new StatisticService(data);
            var playerService = new PlayerService(data, mapper, statisticService);
            //Act

            var result = playerService.AdminView();

            //Assert
            Assert.True(result.Players.Count==10);
            Assert.True(result.Statistics["Total players"] == 10);
            Assert.True(result.Players.Select(x => x.Champions.Count == 1).ToList().Count == 10);

        }

    }
}
