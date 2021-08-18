namespace DarkBattle.Tests.Mocks
{
    using AutoMapper;
    using DarkBattle.Services.MappingConfiguration;
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<DarkBattleProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
