using System.ComponentModel.DataAnnotations;
using static DeskMarket.Common.ModelConstants.Category;

namespace DeskMarket.Data.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
	}

	//•	Has Id – a unique integer, Primary Key
	//•	Has Name – a string with min length 3 and max length 20 (required)
	//•	Has Products – a collection of type Product
}
