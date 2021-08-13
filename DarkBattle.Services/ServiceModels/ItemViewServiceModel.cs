namespace DarkBattle.Services.ServiceModels
{
    using System.ComponentModel;

    public class ItemViewServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        [DisplayName("Obtain by")]
        public string ObtainBy { get; set; }

        [DisplayName("Required level")]
        public int RequiredLevel { get; set; }

        public int Value { get; set; }

        public bool Equipped { get; set; }

    }
}
