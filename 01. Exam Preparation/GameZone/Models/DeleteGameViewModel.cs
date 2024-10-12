using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
	public class DeleteGameViewModel
	{
		public int Id { get; set; }

		[Required] public string Title { get; set; } = null!;

		[Required]
		public string Publisher { get; set; } = null!;
	}
}
