namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Consumables;
    public class ConsumableServiceTest
    {

        [Fact]
        public void TestConsumableAdd()
        {
            //Arrange
            const string name = "Test";
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var model = new ConsumableViewServiceModel() { Name = name };

            var consumableService = new ConsumableService(data, mapper);
            //Act
            consumableService.Add(model);
            var result = data.Consumables.FirstOrDefault(x => x.Name == name);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void TestConsumableEdit()
        {
            //Arrange
            const string name = "Test";
            const string id = "TestId";
            const int healthRestore = 5;
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var consumable = new Consumable { Id = id, Name = name };
            data.Consumables.Add(consumable);
            data.SaveChanges();
            var consumableService = new ConsumableService(data, mapper);

            //Act
            var newModel = new ConsumableViewServiceModel() 
            { 
                Id = id,
                RestoreHealth=healthRestore
            };
            consumableService.Edit(newModel);

            var result = data.Consumables.FirstOrDefault();

            //Assert
            Assert.Equal(healthRestore, result.RestoreHealth);
            Assert.Equal(null, result.Name);
        }

        [Fact]
        public void TestConsumableCollection()
        {
            //Arrange
            const int allConsumables = 10;
            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Consumables.AddRange(Enumerable.Range(0, allConsumables).Select(x => new Consumable { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var consumableService = new ConsumableService(data, mapper);
            //Act
            var test1 = consumableService.ConsumablesCollection();
            var test2 = consumableService.ConsumablesWithNoCreature();
            var test3 = consumableService.ConsumablesWithNoMerchant();
            //Assert
            Assert.Equal(allConsumables, test1.Count);
            Assert.Equal(allConsumables, test2.Count);
            Assert.Equal(allConsumables, test3.Count);

        }

    }
}
