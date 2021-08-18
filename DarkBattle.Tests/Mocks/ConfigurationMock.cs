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
                var inMemorySettings = new Dictionary<string, string> 
                {
                        {"GameSettings:InnitialGold", "50"},
                        {"GameSettings:InnitialExpirience", "50"},
                        {"GameSettings:InnitialLevel", "2"},
                        {"GameSettings:MaxLevel", "15"},
                        {"GameSettings:LevelIncrement:99","999" },
                        {"GameSettings:LevelIncrement:100","999" },
                        {"GameSettings:LevelIncrement:101","999" },
                        {"GameSettings:LevelIncrement:102","999" },
                        {"GameSettings:LevelIncrement:103","999" },
                        {"GameSettings:LevelIncrement:104","999" },
                        {"GameSettings:LevelIncrement:105","999" },
                        {"GameSettings:LevelIncrement:106","999" },
                        {"GameSettings:LevelIncrement:107","999" },
                        {"GameSettings:LevelIncrement:108","999" },
                        {"GameSettings:LevelIncrement:109","999" },
                        {"GameSettings:LevelIncrement:110","999" },
                        {"GameSettings:LevelIncrement:111","999" },
                        {"GameSettings:LevelIncrement:112","999" },
                        {"GameSettings:LevelIncrement:113","999" },
                        {"GameSettings:LevelIncrement:114","999" },
                };

                IConfiguration configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();

                return configuration;
            }
        }
    }
}
