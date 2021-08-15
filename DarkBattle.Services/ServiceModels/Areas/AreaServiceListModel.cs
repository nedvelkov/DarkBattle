namespace DarkBattle.Services.ServiceModels.Areas
{
    public class AreaServiceListModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public int MinLevel { get; set; }

        public int MaxLevel { get; set; }
    }
}
