namespace DarkBattle.Data
{
    using DarkBattle.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DarkBattleDbContext:DbContext
    {
        public DbSet<Item> Items { get; init; }
        public DbSet<Champion> Champions { get; init; }
        public DbSet<Creature> Creatures { get; init; }
        public DbSet<Merchant> Merchants { get; init; }
        public DbSet<Consumable> Consumables { get; init; }
        public DbSet<Area> Areas { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
