namespace DarkBattle.Tests.Services
{
    using System.Linq;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Champions;

    public class BattleServiceTest
    {
        [Fact]
        public void TestFight()
        {
            //Arrange
            const string championId = "z1";
            const string classId = "zzt";
            const string championName = "zzz";
            const string playerId = "SJ";
            const string creatureId = "w1";
            const string creatureName = "wolf";
            const string areaId = "a2";
            const int profit = 50;

            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;
            var mapper = MapperMock.Instance;

            var championClass = new ChampionClass
            {
                Id = classId,
                Name = championName,
                Agility = 1,
                Health = 100,
                Strenght = 5,
                SpellPower = 25,
            };
            var champion = new Champion
            {
                Id = championId,
                ChampionClass = championClass,
                ChampionClassId = classId,
                Level = 5,
                CurrentHealth=10
            };
            var player = new Player { Id = playerId };
            player.Champions.Add(champion);
            var area = new Area { Id = areaId,MinLevelEnterence=1,MaxLevelCreatures=10 };
            var creature = new Creature
            {
                Id = creatureId,
                Name = creatureName,
                Gold = profit,
                Attack = 1,
                Block = 1,
                Health = 10,
                Defense = 1,
                Area = area,
                AreaId = areaId
            };

            data.Users.Add(player);
            data.Creatures.Add(creature);

            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var areaCreatureService = new AreaCreatureService(data);
            var creatureService = new CreatureService(data, mapper);
            var areaService = new AreaService(data, mapper);
            var battleService = new BattleService(data, championService, mapper, areaCreatureService, config, creatureService, areaService);

            //Act
            var result = battleService.FightWithCreature(championId, areaId, playerId);

            //Assert
            Assert.True(result.Champion.Gold == profit);

        }

        [Theory]
        [InlineData(200000)]
        public void TestTraining(int gold)
        {
            //Arrange
            const string championId = "z1";
            const string classId = "zzt";
            const string championName = "zzz";
            const string playerId = "SJ";

            using var data = DatabaseMock.Instance;
            var config = ConfigurationMock.Configuration;
            var mapper = MapperMock.Instance;

            var championClass = new ChampionClass
            {
                Id = classId,
                Name = championName,
                Agility = 1,
                Health = 100,
                Strenght = 5,
                SpellPower = 25,
            };
            var champion = new Champion
            {
                Id = championId,
                ChampionClass = championClass,
                ChampionClassId = classId,
                Level = 5,
                CurrentHealth = 10,
                Gold=gold
            };
            var player = new Player { Id = playerId };
            player.Champions.Add(champion);

            data.Users.Add(player);

            data.SaveChanges();

            var championService = new ChampionService(config, data, mapper);
            var areaCreatureService = new AreaCreatureService(data);
            var creatureService = new CreatureService(data, mapper);
            var areaService = new AreaService(data, mapper);
            var battleService = new BattleService(data, championService, mapper, areaCreatureService, config, creatureService, areaService);

            //Act
            var result = battleService.TrainChampion(championId, playerId,gold.ToString());

            //Assert
            Assert.True(result.Champion.Level == 15);
            Assert.True(result.Champion.Experience == 999);

        }
    }
}
