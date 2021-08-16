namespace DarkBattle.Services.Models
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;

    public class CreatureItemsService : ICreatureItemsService
    {
        private readonly ApplicationDbContext data;

        public CreatureItemsService(ApplicationDbContext data) 
            => this.data = data;

        public bool Add(string itemId, string creatureId)
        {
            var creature = Creature(creatureId);
            var item = Item(itemId);
            if (creature.Items.Contains(item))
            {
                return false;
            }

            creature.Items.Add(item);

            this.data.SaveChanges();

            return true;
        }

        public bool Remove(string itemId, string creatureId)
        {
            var creature = Creature(creatureId);
            var item = Item(itemId);
            if (creature.Items.Contains(item) == false)
            {
                return false;
            }

            creature.Items.Remove(item);

            this.data.SaveChanges();

            return true;
        }

        private Item Item(string itemId)
            => this.data.Items.FirstOrDefault(x => x.Id == itemId);
        private Creature Creature(string creatureId)
            => this.data
                   .Creatures
                   .Include(x => x.Items)
                   .FirstOrDefault(x => x.Id == creatureId);

    }
}
