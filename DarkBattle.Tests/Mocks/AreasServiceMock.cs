namespace DarkBattle.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using DarkBattle.Services.Interface;
    using Moq;
    using DarkBattle.ViewModels.Areas;

    public class AreasServiceMock
    {
        public static IAreaService Instance
        {
            get
            {
                var areaServiceMock = new Mock<IAreaService>();
                areaServiceMock
                    .Setup(x=>x.AreasCollection())
                    .Returns(new List<AreasListViewModel>
                    {

                    });
                return areaServiceMock.Object;
            }
        }
    }
}
