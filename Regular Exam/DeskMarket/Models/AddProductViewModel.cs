using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ModelConstants.Product;

namespace DeskMarket.Models
{
	public class AddProductViewModel
	{
		[Required]
		[MaxLength(ProductNameMaxLength)]
		[MinLength(ProductNameMinLength)]
		public string ProductName { get; set; } = null!;

		[Required]
		[Range((double)PriceMinValue, (double)PriceMaxValue)]
		public decimal Price { get; set; }

		[Required]
		[MaxLength(DescriptionMaxLength)]
		[MinLength(DescriptionMinLength)]
		public string Description { get; set; } = null!;

		public string? ImageUrl { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = DateTimeFormat)]
		public string AddedOn { get; set; } = DateTime.Today.ToString(DateTimeFormat);

		[Range(1, int.MaxValue)]
		public int CategoryId { get; set; }

		public virtual IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
	}
}
