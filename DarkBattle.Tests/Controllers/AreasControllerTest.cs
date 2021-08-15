namespace DarkBattle.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyTested.AspNetCore.Mvc;
    using DarkBattle.Areas.Admin.Controllers;
    using Xunit;
    using DarkBattle.Tests.Mocks;
    using System.Web.Mvc;

    public class AreasControllerTest
    {
        [Fact]
        public void TestAdminAreaIndexPage()
        {
            //Arrange
            var controller = new AreasController(AreasServiceMock.Instance);

            //
            var result = controller.Index();

            Assert.NotNull(result);
            
        }
                
    }
} 
