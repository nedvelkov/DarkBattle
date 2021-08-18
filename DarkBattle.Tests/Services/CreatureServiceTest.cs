namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Creatures;

    public class CreatureServiceTest
    {
        [Fact]
        public void TestGetCreatuteById()
        {
            //Arrange
            const string creatureId = "Test";

            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Creatures.Add(new Creature { Id = creatureId });
            data.SaveChanges();

            var creatureService = new CreatureService(data, mapper);

            //Act
            var result = creatureService.GetCreature(creatureId);

            //Assert
            Assert.True(result.Id == creatureId);
            Assert.IsType<CreatureServiceModel>(result);
        }

        [Fact]
        public void TestGetCreatureCollection()
        {
            //Arrange
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.Creatures.AddRange(Enumerable.Range(0, 10).Select(x => new Creature { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var creatureService = new CreatureService(data, mapper);

            //Act
            var result = creatureService.CreaturesCollection();

            //Assert
            Assert.IsType<List<CreatureServiceListModel>>(result);

        }

        [Fact]
        public void TestAddNewCreature()
        {
            //Arrange
            const string name = "Test";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            var model = new CreatureServiceModel() { Name = name };

            var creatureService = new CreatureService(data, mapper);

            //Act
            creatureService.Add(model);
            var result = data.Creatures.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Name==name);

        }

        [Fact]
        public void TestEditCreature()
        {
            //Arrange
            const string name = "Test";
            const string id = "id";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var creature = new Creature { Id=id, Name = name };
            data.Creatures.Add(creature);
            data.SaveChanges();

            var creatureService = new CreatureService(data, mapper);

            //Act
            var model = creatureService.GetCreature(id);
            model.Level = 5;
            creatureService.Edit(model);
            var result = data.Creatures.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Level==5);
        }

        [Fact]
        public void TestDeleteCreature()
        {
            //Arrange
            const string id = "testId";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.Creatures.Add(new Creature { Id = id });
            data.SaveChanges();

            var creatureService = new CreatureService(data, mapper);

            //Act
            var result = creatureService.Delete(id);
            var test2 = data.Creatures.FirstOrDefault();

            //Assert
            Assert.True(result);
            Assert.Null(test2);
        }

    }
}
