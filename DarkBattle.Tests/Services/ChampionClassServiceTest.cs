namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;
    using AutoMapper;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.MappingConfiguration;
    using DarkBattle.Services.ServiceModels.ChampionClass;

    public class ChampionClassServiceTest
    {
        private readonly IMapper mapper;
        public ChampionClassServiceTest()
        {
            this.mapper = this.Mapper();
        }
        [Fact]
        public void TestGetChampionClass()
        {
            //Arrange
            const string classId = "TestChampionClass";

            using var data = DatabaseMock.Instance;

            data.ChampionClasses.Add(new ChampionClass { Id = classId });
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, this.mapper);

            //Act
            var result = championClassService.GetClass(classId);

            //Assert
            Assert.True(result.Id == classId);
            Assert.IsType<ChampionClassServiceModel>(result);
        }

        [Fact]
        public void TestAddChampionClass()
        {
            //Arrange
            const string name = "Test";

            using var data = DatabaseMock.Instance;
            var model = new ChampionClassServiceModel
            {
                Name = name
            };

            var championClassService = new ChampionClassService(data, this.mapper);

            //Act
            championClassService.Add(model);
            var result = data.ChampionClasses.First();

            //Assert
            Assert.True(result.Name == name);
        }

        [Fact]
        public void TestEdiChampionClass()
        {
            //Arrange
            const string classId = "TestChampionClass";
            const string name = "Test";
            const int health = 5;

            using var data = DatabaseMock.Instance;
            data.ChampionClasses.Add(new ChampionClass { Id = classId, Name = name }) ;
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, this.mapper);

            //Act
            var model = new ChampionClassServiceModel
            {
                Id = classId,
                Health=health,
            };
            championClassService.Edit(model);
            var result = data.ChampionClasses.First();

            //Assert
            Assert.True(result.Name == null);
            Assert.True(result.Health == health);
        }

        [Fact]
        public void TestChampionClassCollection()
        {
            //Arrange
            const string classId = "TestChampionClass";
            const string name = "Test";
            const int health = 5;

            using var data = DatabaseMock.Instance;
            data.ChampionClasses.Add(new ChampionClass { Id = classId, Name = name });
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, this.mapper);

            //Act
            var model = new ChampionClassServiceModel
            {
                Id = classId,
                Health = health,
            };
            championClassService.Edit(model);
            var result = data.ChampionClasses.First();

            //Assert
            Assert.True(result.Name == null);
            Assert.True(result.Health == health);
        }

        private IMapper Mapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(DarkBattleProfile)));
            return new Mapper(configuration);
        }
    }
}
