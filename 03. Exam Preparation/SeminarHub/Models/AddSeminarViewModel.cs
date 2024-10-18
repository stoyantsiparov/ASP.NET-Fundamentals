using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
	public class AddSeminarViewModel
	{
		[Required]
		[MinLength(3)]
		[MaxLength(100)]
		public string Topic { get; set; } = string.Empty;

		[Required]
		[MinLength(5)]
		[MaxLength(60)]
		public string Lecturer { get; set; } = string.Empty;

		[Required]
		[MinLength(10)]
		[MaxLength(500)]
		public string Details { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "dd/MM/yyyy HH:mm", ApplyFormatInEditMode = true)]
		public string DateAndTime { get; set; } = DateTime.Today.ToString("dd/MM/yyyy HH:mm");

		[Range(30, 180)]
		public int Duration { get; set; }

		[Range(1, int.MaxValue)]
		public int CategoryId { get; set; }
		public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}
