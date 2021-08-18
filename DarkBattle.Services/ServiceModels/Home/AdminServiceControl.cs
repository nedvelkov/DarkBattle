namespace DarkBattle.Services.ServiceModels.Home
{

    using System.Collections.Generic;

    public class AdminServiceControl
    {
        public Dictionary<string,int> Statistics { get; set; }
       public ICollection<PlayerServiceModel> Players { get; init; }
    }
}
