namespace DarkBattle.ViewModels.Items
{
    using System.ComponentModel;

    public class ItemsListView
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        [DisplayName("Obtain by")]
        public string ObtainBy { get; set; }

        [DisplayName("Required level")]
        public int RequiredLevel { get; set; }

        public string ImageUrl { get; set; }
    }
}
