namespace DarkBattle.DataConstants
{
    public class Constants
    {
        //Common constants
        public const string ImageRegex = @"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)";
        public const int NameMinLenght = 3;
        public const int NameMaxLenght = 15;
        public const int MinValue = 1;
        public const int MaxGoldDrop = 250;
        public const int DescriptionMaxLenght = 250;

        //Creature
        public const int MaxLevelCreature = 50;

        //Consumable
        public const int MaxHealthRestore = 350;
    }
}
