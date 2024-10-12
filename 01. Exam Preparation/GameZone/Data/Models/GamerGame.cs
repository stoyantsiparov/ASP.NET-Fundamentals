using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GameZone.Data.Models
{
	public class GamerGame
	{
		[ForeignKey(nameof(Game))]
		public int GameId { get; set; }
		public Game Game { get; set; } = null!;

		[ForeignKey(nameof(Gamer))]
		public string GamerId { get; set; } = null!;
		public IdentityUser Gamer { get; set; } = null!;
	}

	//•	Has GameId – integer, PrimaryKey, foreign key(required)
	//•	Has Game – Game
	//•	Has GamerId – string, PrimaryKey, foreign key(required)
	//•	Has Gamer – IdentityUser

}
