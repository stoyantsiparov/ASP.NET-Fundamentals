namespace DeskMarket.Models
{
	public class AllProductsViewModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; } = null!;
		public decimal Price { get; set; }
		public string Description { get; set; } = null!;
		public string? ImageUrl { get; set; }
		public string AddedOn { get; set; } = null!;
		public string Category { get; set; } = null!;
		public string Seller { get; set; } = null!;
		public bool IsSeller { get; set; }
		public bool HasBought { get; set; }
	}
}
