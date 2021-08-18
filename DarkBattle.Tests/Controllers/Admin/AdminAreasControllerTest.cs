namespace DarkBattle.Tests.Controllers.Admin
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;

    using DarkBattle.Areas.Admin.Controllers;
    using DarkBattle.Tests.Mocks;

    public class AdminAreasControllerTest
    {
        [Fact]
        public void TestAdminAreaIndexPage()
        {
            //Arrange
            var controller = new AreasController(AreasServiceMock.Instance);

            //Act
            var result = controller.Index();
            //Assert
            Assert.NotNull(result);
            
        }

        [Fact]
        public void TestAdminAreaTestCreateArea()
            => MyController<AreasController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(a => a.RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();


    }
} 
