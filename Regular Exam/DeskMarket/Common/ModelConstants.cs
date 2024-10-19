namespace DeskMarket.Common
{
	public static class ModelConstants
	{
		public class Product
		{
			public const int ProductNameMinLength = 2;
			public const int ProductNameMaxLength = 60;
			public const int DescriptionMinLength = 10;
			public const int DescriptionMaxLength = 250;
			public const decimal PriceMinValue = 1.00m;
			public const decimal PriceMaxValue = 3000.00m;
			public const string DateTimeFormat = "dd-MM-yyyy";
		}

		public class Category
		{
			public const int NameMinLength = 3;
			public const int NameMaxLength = 20;
		}
	}
}
