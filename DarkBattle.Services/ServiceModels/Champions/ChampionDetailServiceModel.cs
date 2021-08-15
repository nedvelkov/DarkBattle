namespace DarkBattle.Services.ServiceModels.Champions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DarkBattle.Services.ServiceModels.Consumables;
    using DarkBattle.Services.ServiceModels.Items;

    public class ChampionDetailServiceModel
    {
        public ChampionServiceModel Champion { get; init; }
        public int Strenght { get; set; }

        public int Agility { get; set; }

        [Display(Name = "Current health")]
        public int CurrentHealth { get; set; }

        [Display(Name = "Max health")]
        public int MaxHealth { get; set; }
        [Display(Name = "Spell power")]
        public int SpellPower { get; set; }

        public string Description { get; set; }

        public ICollection<ItemViewServiceModel> Items { get; init; }
        public ICollection<ConsumableViewServiceModel> Consumables { get; init; }

        public GearServiceModel Gear { get; set; }

        public double Attack => (this.PrimaryStat() * 0.2 + this.Agility) * 0.5 + AttackFromItems();
        public double Defense => (this.MaxHealth * 0.5 + Math.Max(this.Agility, this.Strenght)*0.5) * 0.05 + DefenseFromItems();

        [Display(Name = "Crit chanse")]
        public double CritChanse => this.PrimaryStat() * 0.05 + this.Champion.Level * 0.01;
        public double Block => ((this.Strenght * 2 + this.MaxHealth) * 0.03 * (this.Agility * this.Champion.Level) * 0.02) * 0.5;

        private int PrimaryStat()
        {
            var value1 = Math.Max(this.Strenght, this.Agility);
            var value2 = Math.Max(this.SpellPower, this.Agility);
            return Math.Max(value1, value2);
        }

        private double AttackFromItems()
            => this.Gear.EquipedItems.Select(x => x.Attack).Sum();

        private double DefenseFromItems()
            => this.Gear.EquipedItems.Select(x => x.Defense).Sum();

    }
}
