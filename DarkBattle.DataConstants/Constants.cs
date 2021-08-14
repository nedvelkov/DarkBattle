namespace DarkBattle.DataConstants
{
    public class Constants
    {
        //Common constants
        public const string ImageRegex = @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)";
        public const int NameMinLenght = 3;
        public const int NameMaxLenght = 20;
        public const int MinValue = 1;
        public const int MaxGoldDrop = 2500;
        public const int DescriptionMaxLenght = 250;

        //Creature
        public const int MaxLevelCreature = 50;
        public const int MaxAttackCreature = 2500;
        public const int MaxDefensekCreature = 3500;
        public const int MaxHealthCreature = 2500;

        //Consumable
        public const int MaxHealthRestore = 500;

        //Item
        public const int MaxItemAttack = 1000;
        public const int MaxItemDefense = 1000;

        //Champion
        public const int MaxChampionLevel = 50;

    }
}
