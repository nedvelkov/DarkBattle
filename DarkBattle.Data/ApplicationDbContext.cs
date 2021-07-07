namespace DarkBattle.Data
{
    using DarkBattle.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; init; }
        public DbSet<Champion> Champions { get; init; }
        public DbSet<Creature> Creatures { get; init; }
        public DbSet<Merchant> Merchants { get; init; }
        public DbSet<Consumable> Consumables { get; init; }
        public DbSet<Area> Areas { get; init; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
