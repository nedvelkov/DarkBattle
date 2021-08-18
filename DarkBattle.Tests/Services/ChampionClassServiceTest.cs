namespace DarkBattle.Tests.Services
{

    using System.Linq;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;

    using DarkBattle.Services.ServiceModels.ChampionClass;

    public class ChampionClassServiceTest
    {

        [Fact]
        public void TestGetChampionClass()
        {
            //Arrange
            const string classId = "TestChampionClass";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.ChampionClasses.Add(new ChampionClass { Id = classId });
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, mapper);

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

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            var model = new ChampionClassServiceModel
            {
                Name = name
            };

            var championClassService = new ChampionClassService(data, mapper);

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

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            data.ChampionClasses.Add(new ChampionClass { Id = classId, Name = name }) ;
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, mapper);

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
            const int numOfClass= 4;
            const string name = "Test";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;
            for (int i = 0; i < numOfClass; i++)
            {
                var @class = new ChampionClass { Name = $"{name}{i}" };
                data.ChampionClasses.Add(@class);
            }
            data.SaveChanges();

            var championClassService = new ChampionClassService(data, mapper);

            //Act

            var result = championClassService.ChampionClassCollection();

            //Assert
            var listNames = result.ToList();

            Assert.True(listNames.Count == numOfClass);
            Assert.True(listNames[0] == $"{name}0");
        }
    }
}
