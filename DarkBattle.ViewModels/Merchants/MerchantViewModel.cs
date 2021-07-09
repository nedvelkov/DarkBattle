
namespace DarkBattle.ViewModels.Merchants
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Constants;

    public class MerchantViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(NameMinLenght)]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]

        public string Description { get; set; }

        [Required]
        [DisplayName("Image")]
        [RegularExpression(ImageRegex, ErrorMessage = "Provided url addres is not correct.")]
        public string ImageUrl { get; set; }
    }
}
