namespace DarkBattle.Services.ServiceModels
{
    using System.ComponentModel.DataAnnotations;
    public class ConsumableViewServiceModel
    {
        public string Id { get; init; }

        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Restore healt")]
        public int RestoreHealth { get; set; }

        public int Value { get; set; }
    }
}
