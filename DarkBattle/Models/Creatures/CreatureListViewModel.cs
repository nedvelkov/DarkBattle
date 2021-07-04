namespace DarkBattle.Models.Creatures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class CreatureListViewModel
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public int Level { get; set; }
        public string Area { get; set; }


    }
}
