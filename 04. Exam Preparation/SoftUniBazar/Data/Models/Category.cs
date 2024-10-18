using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Common.ModelConstants;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}
