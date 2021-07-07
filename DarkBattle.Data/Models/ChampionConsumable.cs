
namespace DarkBattle.Data.Models
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    public class ChampionConsumable
    {
        public string ChampionId { get; set; }
        public Champion Champion { get; set; }

        public string ConsumableId { get; set; }
        public Consumable Consumable { get; set; }
    }
}
