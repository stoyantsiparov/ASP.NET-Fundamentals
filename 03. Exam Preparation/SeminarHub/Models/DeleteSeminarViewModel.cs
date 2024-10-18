using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
	public class DeleteSeminarViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Topic { get; set; } = null!;

		[Required]
		public DateTime DateAndTime { get; set; }
	}
}
