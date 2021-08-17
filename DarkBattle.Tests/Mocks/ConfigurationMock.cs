namespace DarkBattle.Tests.Mocks
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationMock
    {
        public static IConfiguration Configuration
        {
            get
            {
                var inMemorySettings = new Dictionary<string, string> {
                        {"GameSettings:InnitialGold", "50"},
                        {"GameSettings:InnitialExpirience", "50"},
                        {"GameSettings:InnitialLevel", "2"},
                    };

                IConfiguration configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                return configuration;
            }
        }
    }
}
