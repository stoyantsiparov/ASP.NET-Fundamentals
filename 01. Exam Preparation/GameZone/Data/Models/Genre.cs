using System.ComponentModel.DataAnnotations;

namespace GameZone.Data.Models
{
	public class Genre
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(25)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
	}

	//  •	Has Id – a unique integer, Primary Key
	//	•	Has Name – a string with min length 3 and max length 25 (required)
	//	•	Has Games – a collection of type Game

}
