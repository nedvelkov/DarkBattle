namespace DarkBattle.Services.Models
{

    using System.Linq;

    using DarkBattle.Data;
    using DarkBattle.Services.Interface;



    public class AreaCreatureService : IAreaCreatureService
    {
        private readonly ApplicationDbContext data;


        public AreaCreatureService(ApplicationDbContext data) 
            => this.data = data;

        public void Add(string creatureId, string areaId)
        {
            var creatureAsQuareable = this.data.Creatures.Single(x => x.Id == creatureId);

            creatureAsQuareable.AreaId = areaId;

            this.data.SaveChanges();
        }

        public void Remove(string creatureId, string areaId)
        {
            var all = this.data.Creatures.ToList();
            var creatureAsQuareable = this.data.Creatures.Single(x => x.Id == creatureId);
            if (creatureAsQuareable.AreaId==areaId)
            {
            creatureAsQuareable.AreaId = null;
            }

            this.data.SaveChanges();
        }

    }
}
