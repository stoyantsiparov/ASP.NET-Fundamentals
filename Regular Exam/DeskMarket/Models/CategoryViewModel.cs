using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ModelConstants.Category;

namespace DeskMarket.Models
{
	public class CategoryViewModel
	{
		public int Id { get; set; }

		[MaxLength(NameMaxLength)]
		[MinLength(NameMinLength)]
		public required string Name { get; set; } = null!;
	}
}
