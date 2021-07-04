using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBattle.Data.Models
{
    public class ChampionItem
    {
        public string ChampionId { get; set; }
        public Champion Champion { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }

    }
}
