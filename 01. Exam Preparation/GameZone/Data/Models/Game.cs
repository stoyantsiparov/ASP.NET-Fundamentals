using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Data.Models
{
	public class Game
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(500)]
		public string Description { get; set; } = null!;

		public string? ImageUrl { get; set; }

		[ForeignKey(nameof(Publisher))]
		public string PublisherId { get; set; } = null!;

		[Required]
		public IdentityUser Publisher { get; set; } = null!;

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime ReleasedOn { get; set; }

		[ForeignKey(nameof(Genre))]
		public int GenreId { get; set; }
		public Genre Genre { get; set; } = null!;
		public virtual ICollection<GamerGame> GamersGames { get; set; } = new HashSet<GamerGame>();
	}

	//•	Has Id – a unique integer, Primary Key
	//•	Has Title – a string with min length 2 and max length 50 (required)
	//•	Has Description – string with min length 10 and max length 500 (required)
	//•	Has ImageUrl – nullable string
	//•	Has PublisherId – string (required)
	//•	Has Publisher – IdentityUser(required)
	//•	Has ReleasedOn– DateTime with format " yyyy-MM-dd" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
	//•	Has GenreId – integer, foreign key (required)
	//•	Has Genre – Genre (required)
	//•	Has GamersGames – a collection of type GamerGame
}
