using DeskMarket.Models;

namespace DeskMarket.Contracts
{
	public interface IDeskMarket
	{
		Task<IEnumerable<AllProductsViewModel>> GetAllProductsAsync(string userId);
		Task<IEnumerable<AllProductsViewModel>> GetMyProductsAsync(string userId);
		Task<ProductViewModel?> GetProductIdAsync(int id);
		Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id);
		Task<bool> HasUserBoughtProductAsync(string userId, int productId);
		Task AddToCartAsync(string userId, ProductViewModel product);
		Task RemoveFromCartAsync(string userId, ProductViewModel product);
		Task<AddProductViewModel> GetProductForAddAsync();
		Task AddProductAsync(AddProductViewModel model, string userId);
		Task EditProductAsync(ProductViewModel model);
		Task<DeleteProductViewModel?> GetProductForDeleteAsync(int id);
		Task SoftDeleteProductAsync(int id);
	}
}