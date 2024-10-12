using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
	public class GameViewModel
	{
		public int Id { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Title { get; set; } = null!;

		public string? ImageUrl { get; set; }

		[Required]
		[MinLength(10)]
		[MaxLength(500)]
		public string Description { get; set; } = null!;

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string ReleasedOn { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

		[Range(1, int.MaxValue)]
		public int GenreId { get; set; }

		public virtual IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
	}
}
