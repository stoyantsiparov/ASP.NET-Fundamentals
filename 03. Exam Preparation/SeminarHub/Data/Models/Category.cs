using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Seminar> Seminars { get; set; } = new HashSet<Seminar>();
    }
    //    •	Has Id – a unique integer, Primary Key
    //    •	Has Name – string with min length 3 and max length 50 (required)
    //    •	Has Seminars – a collection of type Seminar

}
