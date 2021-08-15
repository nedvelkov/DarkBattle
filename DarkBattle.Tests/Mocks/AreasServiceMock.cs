namespace DarkBattle.Tests.Mocks
{

    using System.Collections.Generic;

    using DarkBattle.Services.Interface;
    using Moq;
    using DarkBattle.Services.ServiceModels.Areas;

    public class AreasServiceMock
    {
        public static IAreaService Instance
        {
            get
            {
                var areaServiceMock = new Mock<IAreaService>();
                areaServiceMock
                    .Setup(x=>x.AreasCollection())
                    .Returns(new List<AreaServiceListModelExtention>
                    {

                    });
                return areaServiceMock.Object;
            }
        }
    }
}
