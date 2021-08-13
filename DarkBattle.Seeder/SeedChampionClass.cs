namespace DarkBattle.Seeder
{
    using System.Collections.Generic;
    using DarkBattle.Data;
    using DarkBattle.Data.Models;
    public class SeedChampionClass
    {
        private readonly ApplicationDbContext data;

        public SeedChampionClass(ApplicationDbContext data) => this.data = data;

        public void Seed()
        {
            var warrior = new ChampionClass
            {
                Name = "Warrior",
                Strenght = 13,
                Agility = 8,
                Health = 10,
                SpellPower=5,
                ImageUrl = @"https://guides.gamepressure.com/guildwars2/gfx/word/978182984.jpg",
                Description = "Fearless warrior, with massive physical damage."
            };
            var mage = new ChampionClass
            {
                Name = "Mage",
                Strenght = 5,
                Agility = 8,
                Health = 7,
                SpellPower=15,
                ImageUrl = @"https://rpgvaliant.com/wp-content/uploads/2017/02/Final-Fantasy-Explorers-Force-Black-Mage.jpg",
                Description = "Wise wizard with powers over nature."
            }; 
            var rouge = new ChampionClass
            {
                Name = "Rouge",
                Strenght = 6,
                Agility = 14,
                Health = 6,
                SpellPower=9,
                ImageUrl = @"https://i.pinimg.com/originals/7b/7f/1f/7b7f1fcdcaf7453614dfdf448ff284ce.jpg",
                Description = "Stealth and silent, rouge attack from shadows."
            };
            var archer = new ChampionClass
            {
                Name = "Archer",
                Strenght = 10,
                Agility = 14,
                Health = 8,
                SpellPower=9,
                ImageUrl = @"https://vignette.wikia.nocookie.net/raiderz-game/images/2/29/Archer.png",
                Description = "Agile, quick and silent archer always find his prey."
            };
            var classes = new List<ChampionClass>()
            {
                warrior,mage,rouge,archer
            };

            this.data.ChampionClasses.AddRange(classes);
            this.data.SaveChanges();
        }
    }
}
