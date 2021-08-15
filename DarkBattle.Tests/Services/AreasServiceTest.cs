namespace DarkBattle.Tests.Services
{

    using System.Linq;

    using Xunit;
    using AutoMapper;

    using MyTested.AspNetCore.Mvc;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.ViewModels.Areas;
    using DarkBattle.Services.MappingConfiguration;
    using System.Collections.Generic;
    using System;

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
            Assert.IsType<AreaViewModel>(result);
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
            Assert.IsType<List<AreasListViewModel>>(result);

        }

        [Fact]
        public void TestAddNewArea()
        {
            //Arrange
            const string name = "Test";
            using var data = DatabaseMock.Instance;

            var model = new AreaViewModel() { Name = name };

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

            var model = new AreaViewModel() { Name = name };

            var areaService = new AreaService(data, this.mapper);
            //Act
            areaService.Add(model);
            var result = data.Areas.FirstOrDefault(x => x.Name == name);

            //Assert
            Assert.NotNull(result);
        }



        private IMapper Mapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(typeof(DarkBattleProfile)));
            return new Mapper(configuration);
        }
    }
}
