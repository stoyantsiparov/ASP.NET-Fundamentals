using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
	public class GameDetailsViewModel
	{
		public int Id { get; set; }
		public string? ImageUrl { get; set; }

		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Title { get; set; } = null!;

		[Required]
		[MinLength(10)]
		[MaxLength(500)]
		public string Description { get; set; } = null!;

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string ReleasedOn { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");

		[Required]
		public string Genre { get; set; } = null!;

		[Required]
		public string Publisher { get; set; } = null!;
	}
}
