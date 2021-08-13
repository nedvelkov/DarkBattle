namespace DarkBattle.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class Player: IdentityUser
    {
        public bool IsBanned { get; set; }
        public string BanMessage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsTraining { get; set; }
        public string TrainingMessage { get; set; }
        public bool IsOnline { get; set; }

        public ICollection<Champion> Champions { get; set; } = new HashSet<Champion>();
    }
}
