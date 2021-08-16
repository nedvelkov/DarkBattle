namespace DarkBattle.Services.Models
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    using DarkBattle.Services.Interface;

    public class CreatureConsumablesService : ICreatureConsumablesService
    {
        private readonly ApplicationDbContext data;


        public CreatureConsumablesService(ApplicationDbContext data)
            => this.data = data;

        public bool Add(string consumableId, string creatureId)
        {
            var creature = Creature(creatureId);
            var consumable = Consumable(consumableId);
            if (creature.Consumables.Contains(consumable))
            {
                return false;
            }

            creature.Consumables.Add(consumable);

            this.data.SaveChanges();

            return true;
        }

        public bool Remove(string consumableId, string creatureId)
        {
            var creature = Creature(creatureId);
            var consumable = Consumable(consumableId);
            if (creature.Consumables.Contains(consumable) == false)
            {
                return false;
            }

            creature.Consumables.Remove(consumable);

            this.data.SaveChanges();

            return true;
        }

        private Consumable Consumable(string consumableId)
            => this.data.Consumables.FirstOrDefault(x => x.Id == consumableId);

        private Creature Creature(string creatureId)
            => this.data
                   .Creatures
                   .Include(x => x.Consumables)
                   .FirstOrDefault(x => x.Id == creatureId);
    }
}
