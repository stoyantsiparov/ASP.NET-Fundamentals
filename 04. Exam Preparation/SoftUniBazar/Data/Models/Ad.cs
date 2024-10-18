using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static SoftUniBazar.Common.ModelConstants;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = AdCreatedOn, ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
