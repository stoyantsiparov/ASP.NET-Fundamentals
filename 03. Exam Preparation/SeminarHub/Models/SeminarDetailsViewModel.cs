using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class SeminarDetailsViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Topic { get; set; } = null!;

        [Required]
        [MinLength(5)]
        [MaxLength(60)]
        public string Lecturer { get; set; } = null!;

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Details { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm", ApplyFormatInEditMode = true)]
        public string DateAndTime { get; set; } = DateTime.Today.ToString("dd/MM/yyyy HH:mm");

        [Range(30, 180)] public int Duration { get; set; }

        [Required] public string Category { get; set; } = null!;

        [Required] public string Organizer { get; set; } = null!;
    }
}
