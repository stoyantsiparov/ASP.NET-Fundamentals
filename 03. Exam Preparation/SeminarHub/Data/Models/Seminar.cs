using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(60)]
        public string Lecturer { get; set; } = null!;

        [Required] [MaxLength(500)] public string Details { get; set; } = null!;

        [ForeignKey(nameof(Organizer))] 
        public string OrganizerId { get; set; } = null!;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm", ApplyFormatInEditMode = true)]
        public DateTime DateAndTime { get; set; }

        [Range(30, 180)]
        public int Duration { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public virtual ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new HashSet<SeminarParticipant>();
    }
    //    •	Has Id – a unique integer, Primary Key
    //    •	Has Topic – string with min length 3 and max length 100 (required)
    //    •	Has Lecturer – string with min length 5 and max length 60 (required)
    //    •	Has Details – string with min length 10 and max length 500 (required)
    //    •	Has OrganizerId – string (required)
    //    •	Has Organizer – IdentityUser(required)
    //    •	Has DateAndTime – DateTime with format "dd/MM/yyyy HH:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
    //    •	Has Duration – integer value between 30 and 180
    //    •	Has CategoryId – integer, foreign key (required)
    //    •	Has Category – Category (required)
    //    •	Has SeminarsParticipants – a collection of type SeminarParticipant

    }
