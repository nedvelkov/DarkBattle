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
    using DarkBattle.Services.ServiceModels.Areas;

    public class AreasServiceTest
    {
        private readonly IMapper mapper;
        public AreasServiceTest()
        {
            this.mapper = this.Mapper();
        }

        [Fact]
        public void TestGetAreaById()
        {
            //Arrange
            const string areaId = "TestArea";

            using var data = DatabaseMock.Instance;

            data.Areas.Add(new Area { Id = areaId });
            data.SaveChanges();

            var areaService = new AreaService(data, this.mapper);

            //Act
            var result = areaService.GetArea(areaId);

            //Assert
            Assert.True(result.Id == areaId);
            Assert.IsType<AreaServiceViewModel>(result);
        }

        [Fact]
        public void TestGetAreasCollection()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            data.Areas.AddRange(Enumerable.Range(0, 10).Select(x => new Area { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var areaService = new AreaService(data, this.mapper);
            //Act
            var result = areaService.AreasCollection();

            //Assert
            Assert.IsType<List<AreaServiceListModelExtention>>(result);

        }

        [Fact]
        public void TestAddNewArea()
        {
            //Arrange
            const string name = "Test";
            using var data = DatabaseMock.Instance;

            var model = new AreaServiceViewModel() { Name = name };

            var areaService = new AreaService(data, this.mapper);
            //Act
            areaService.Add(model);
            var result = data.Areas.FirstOrDefault(x => x.Name == name);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void TestEditArea()
        {   
            //Arrange
            const string name = "Test";
            using var data = DatabaseMock.Instance;

            var model = new AreaServiceViewModel() { Name = name };

            var areaService = new AreaService(data, this.mapper);
            //Act
            areaService.Add(model);
            var result = data.Areas.FirstOrDefault(x => x.Name == name);

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TestDeleteArea()
        {
            //Arrange
            const string areaId = "TestArea";

            using var data = DatabaseMock.Instance;

            data.Areas.Add(new Area { Id = areaId });
            data.SaveChanges();

            var areaService = new AreaService(data, this.mapper);

            //Act
            var result = areaService.Delete(areaId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TestGetAreaForCreatures()
        {
            //Arrange
            const string areaId = "TestArea";

            using var data = DatabaseMock.Instance;

            data.Areas.Add(new Area { Id = areaId });
            data.SaveChanges();

            var areaService = new AreaService(data, this.mapper);

            //Act
            var result = areaService.AreaForCreatures(areaId);

            //Assert
            Assert.True(result.Id==areaId);
            Assert.IsType<AreaServiceListModel>(result);

        }

        private IMapper Mapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(DarkBattleProfile)));
            return new Mapper(configuration);
        }
    }
}
