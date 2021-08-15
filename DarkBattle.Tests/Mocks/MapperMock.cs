namespace DarkBattle.Tests.Mocks
{
    using AutoMapper;
    using DarkBattle.Services.MappingConfiguration;
    using Moq;
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperMock = new Mock<IMapper>();

                mapperMock.SetupGet(m => m.ConfigurationProvider)
                    .Returns(Mock.Of<IConfigurationProvider>());
                return mapperMock.Object;
            }
        }
    }
}
