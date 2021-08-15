namespace DarkBattle.Services.ServiceModels.Areas
{
    using DarkBattle.Services.ServiceModels.Creatures;
    using System.Collections.Generic;

    public class AreaServiceModel:AreaServiceViewModel
    {
        public ICollection<CreatureServiceModel> Creatures { get; set; }
    }
}
