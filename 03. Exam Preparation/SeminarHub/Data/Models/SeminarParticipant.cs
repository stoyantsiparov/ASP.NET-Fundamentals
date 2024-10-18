using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SeminarHub.Data.Models
{
    public class SeminarParticipant
    {
        [ForeignKey(nameof(Seminar))]
        public int SeminarId { get; set; }

        public Seminar Seminar { get; set; } = null!;

        [ForeignKey(nameof(Participant))]
        public string ParticipantsId { get; set; } = null!;

        public IdentityUser Participant { get; set; } = null!;
    }
    //•	Has SeminarId – integer, PrimaryKey, foreign key(required)
    //    •	Has Seminar – Seminar
    //    •	Has ParticipantId – string, PrimaryKey, foreign key(required)
    //    •	Has Participant – IdentityUser

}
