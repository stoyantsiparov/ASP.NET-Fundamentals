using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ModelConstants;
namespace SoftUniBazar.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = null!;

    }
}
