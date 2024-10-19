using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ModelConstants.Product;

namespace DeskMarket.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[MaxLength(ProductNameMaxLength)]
		[MinLength(ProductNameMinLength)]
		public required string ProductName { get; set; } = null!;

		[MaxLength(DescriptionMaxLength)]
		[MinLength(DescriptionMinLength)]
		public required string Description { get; set; } = null!;

		[Range((double)PriceMinValue, (double)PriceMaxValue)]
		public required decimal Price { get; set; }

		public string? ImageUrl { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = DateTimeFormat)]
		public required string AddedOn { get; set; } = DateTime.Today.ToString(DateTimeFormat);

		[Range(1, int.MaxValue)]
		public required int CategoryId { get; set; }
		public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();

		public required string SellerId { get; set; } = null!;
	}
}
