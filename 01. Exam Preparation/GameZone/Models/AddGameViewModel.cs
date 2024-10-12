using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
	public class AddGameViewModel
	{
		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Title { get; set; } = string.Empty;

		public string? ImageUrl { get; set; }

		[Required]
		[MinLength(10)]
		[MaxLength(500)]
		public string Description { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string ReleasedOn { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

		[Range(1, int.MaxValue)]
		public int GenreId { get; set; }

		public virtual IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
	}
}
