namespace DarkBattle.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using DarkBattle.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Item> Items { get; init; }
        public DbSet<Champion> Champions { get; init; }
        public DbSet<Creature> Creatures { get; init; }
        public DbSet<Merchant> Merchants { get; init; }
        public DbSet<Consumable> Consumables { get; init; }
        public DbSet<Area> Areas { get; init; }
        public DbSet<ChampionClass> ChampionClasses { get; init; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Area>()
        //        .HasMany(c => c.Creatures)
        //        .WithOne(c => c.Area)
        //        .HasForeignKey(c => c.AreaId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //}
    }
}
