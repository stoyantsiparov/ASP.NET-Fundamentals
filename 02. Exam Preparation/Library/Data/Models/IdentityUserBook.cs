using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Models
{
    public class IdentityUserBook
    {
        [ForeignKey(nameof(Collector))]
        public string CollectorId { get; set; } = null!;
        public IdentityUser Collector { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
