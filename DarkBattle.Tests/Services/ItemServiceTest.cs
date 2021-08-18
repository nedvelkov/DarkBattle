namespace DarkBattle.Tests.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Xunit;

    using DarkBattle.Tests.Mocks;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Models;
    using DarkBattle.Services.ServiceModels.Items;

    public  class ItemServiceTest
    {
        [Fact]
        public void TestGetItemById()
        {
            //Arrange
            const string itemId= "TestId";

            using var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Items.Add(new Item { Id = itemId });
            data.SaveChanges();

            var itemService = new ItemService(data, mapper);

            //Act
            var result = itemService.GetItem(itemId);

            //Assert
            Assert.True(result.Id == itemId);
            Assert.IsType<ItemServiceModel>(result);
        }

        [Fact]
        public void TestGetItemCollection()
        {
            //Arrange
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.Items.AddRange(Enumerable.Range(0, 10).Select(x => new Item { Id = Guid.NewGuid().ToString() }));
            data.SaveChanges();

            var itemService = new ItemService(data, mapper);

            //Act
            var result = itemService.ItemsCollection();

            //Assert
            Assert.IsType<List<ItemServiceListModel>>(result);
            Assert.True(result.Count == 10);

        }

        [Fact]
        public void TestAddNewItem()
        {
            //Arrange
            const string name = "Test";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            var model = new ItemServiceModel() { Name = name };

            var itemService = new ItemService(data, mapper);

            //Act
            itemService.Add(model);
            var result = data.Items.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Name == name);

        }

        [Fact]
        public void TestEditItem()
        {
            //Arrange
            const string name = "Test";
            const string id = "id";
            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.Items.Add(new Item { Id = id, Name = name });
            data.SaveChanges();

            var itemService = new ItemService(data, mapper);

            //Act
            var model = itemService.GetItem(id);
            model.RequiredLevel = 5;
            itemService.Edit(model);
            var result = data.Items.FirstOrDefault();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.RequiredLevel == 5);
        }

        [Fact]
        public void TestDeleteItem()
        {
            //Arrange
            const string id = "testId";

            var mapper = MapperMock.Instance;
            using var data = DatabaseMock.Instance;

            data.Items.Add(new Item { Id = id });
            data.SaveChanges();

            var itemService = new ItemService(data, mapper);

            //Act
            var result = itemService.Delete(id);
            var test2 = data.Items.FirstOrDefault();

            //Assert
            Assert.True(result);
            Assert.Null(test2);
        }
    }
}
