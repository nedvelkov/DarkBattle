﻿namespace DarkBattle.Tests.Mocks
{
    using System;
    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Data;

   public static class DatabaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;
                return new ApplicationDbContext(dbContextOptions);
            }
        }
    }
}
