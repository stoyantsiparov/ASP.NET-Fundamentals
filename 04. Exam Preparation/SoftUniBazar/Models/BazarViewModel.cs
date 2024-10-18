using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ModelConstants;

namespace SoftUniBazar.Models
{
    public class BazarViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AdNameMaxLength, MinimumLength = AdNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
