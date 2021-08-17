namespace DarkBattle.Tests.Services
{

    using System.Linq;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;

    public class AreaCreaturesServiceTest
    {

        [Fact]
        public void TestAddCreatureToArea()
        {
            //Arrange
            const string creatureName = "Zer";
            const string creatureId = "z1";
            const string areaName = "ALand";
            const string areaId = "a1";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureId, Name = creatureName };
            var area = new Area { Id = areaId, Name = areaName };
            data.Areas.Add(area);
            data.Creatures.Add(creature);
            data.SaveChanges();
            var areaCreatureService = new AreaCreatureService(data);

            //Act
            areaCreatureService.Add(creatureId, areaId);
            var test1 = data.Areas
                              .Include(x=>x.Creatures)
                             .FirstOrDefault(x => x.Id == areaId);
            var test2 = data.Creatures.FirstOrDefault(c => c.Id == creatureId).AreaId;

            //Assert
            Assert.Equal(test1.Creatures.First().Id, creatureId);
            Assert.Equal(test2, areaId);

        }

        [Fact]
        public void TestRemoveCreatureToArea()
        {
            //Arrange
            const string creatureName = "Zer";
            const string creatureId = "z1";
            const string areaName = "ALand";
            const string areaId = "a1";

            using var data = DatabaseMock.Instance;

            var creature = new Creature { Id = creatureId, Name = creatureName };
            var area = new Area { Id = areaId, Name = areaName };
            area.Creatures.Add(creature);
            data.Areas.Add(area);
            data.SaveChanges();
            var areaCreatureService = new AreaCreatureService(data);

            //Act
            areaCreatureService.Remove(creatureId, areaId);
            var test = data.Creatures.FirstOrDefault(c => c.Id == creatureId).AreaId;

            //Assert
            Assert.Equal(test, null);

        }

    }
}
