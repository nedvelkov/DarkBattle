namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;

    using Xunit;
    using AutoMapper;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.MappingConfiguration;
    using DarkBattle.Services.ServiceModels.Consumables;
    public class ConsumableServiceTest
    {
        private readonly IMapper mapper;
        public ConsumableServiceTest()
        {
            this.mapper = this.Mapper();
        }

        [Fact]
        public void TestConsumableAdd()
        {
            //Arrange
            const string name = "Test";
            using var data = DatabaseMock.Instance;

            var model = new ConsumableViewServiceModel() { Name = name };

            var consumableService = new ConsumableService(data, this.mapper);
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
            const int healthRestore = 5;
            using var data = DatabaseMock.Instance;

            var model = new ConsumableViewServiceModel() { Name = name };

            var consumableService = new ConsumableService(data, this.mapper);
            consumableService.Add(model);
            var consumableFromDb = data.Consumables.FirstOrDefault(x => x.Name == name);
            //Act
            var newModel= new ConsumableViewServiceModel() 
            { 
                Id = consumableFromDb.Id,
                RestoreHealth=healthRestore
            };
            consumableService.Edit(newModel);

            var result = data.Consumables.FirstOrDefault(x => x.Id == consumableFromDb.Id);

            //Assert
            Assert.Equal(healthRestore, result.RestoreHealth);
        }

        [Fact]
        public void TestConsumableCollection()
        {
            //Arrange
            const int allConsumables = 10;
            using var data = DatabaseMock.Instance;

            data.Consumables.AddRange(Enumerable.Range(0, allConsumables).Select(x => new Consumable { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var consumableService = new ConsumableService(data, this.mapper);
            //Act
            var test1 = consumableService.ConsumablesCollection();
            var test2 = consumableService.ConsumablesWithNoCreature();
            var test3 = consumableService.ConsumablesWithNoMerchant();
            //Assert
            Assert.Equal(allConsumables, test1.Count);
            Assert.Equal(allConsumables, test2.Count);
            Assert.Equal(allConsumables, test3.Count);

        }


        private IMapper Mapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(DarkBattleProfile)));
            return new Mapper(configuration);
        }
    }
}
