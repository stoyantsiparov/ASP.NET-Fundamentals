using DeskMarket.Contracts;
using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models;
using Microsoft.EntityFrameworkCore;
using static DeskMarket.Common.ModelConstants.Product;

namespace DeskMarket.Services
{
	public class DeskMarketService : IDeskMarket
	{
		private readonly ApplicationDbContext _context;

		public DeskMarketService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AllProductsViewModel>> GetAllProductsAsync(string userId)
		{
			return await _context.Products
				.Where(p => p.IsDeleted == false)
				.Select(p => new AllProductsViewModel
				{
					Id = p.Id,
					ProductName = p.ProductName,
					Price = p.Price,
					Description = p.Description,
					ImageUrl = p.ImageUrl,
					AddedOn = p.AddedOn.ToString(DateTimeFormat),
					Category = p.Category.Name,
					Seller = p.Seller.UserName,
					IsSeller = p.SellerId == userId,
					HasBought = _context.ProductsClients.Any(pc => pc.ClientId == userId && pc.ProductId == p.Id)
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<AllProductsViewModel>> GetMyProductsAsync(string userId)
		{
			return await _context.ProductsClients
				.Where(pc => pc.ClientId == userId && !pc.Product.IsDeleted)
				.Select(pc => new AllProductsViewModel
				{
					Id = pc.Product.Id,
					ProductName = pc.Product.ProductName,
					Price = pc.Product.Price,
					Description = pc.Product.Description,
					ImageUrl = pc.Product.ImageUrl,
					AddedOn = pc.Product.AddedOn.ToString(DateTimeFormat),
					Category = pc.Product.Category.Name,
					Seller = pc.Product.Seller.UserName,
					IsSeller = pc.Product.SellerId == userId,
					HasBought = true
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<ProductViewModel?> GetProductIdAsync(int id)
		{
			return await _context.Products
				.Where(p => p.Id == id)
				.Select(p => new ProductViewModel
				{
					Id = p.Id,
					ProductName = p.ProductName,
					Description = p.Description,
					Price = p.Price,
					ImageUrl = p.ImageUrl,
					AddedOn = p.AddedOn.ToString(DateTimeFormat),
					CategoryId = p.CategoryId,
					SellerId = p.SellerId
				})
				.FirstOrDefaultAsync();
		}

		public Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id)
		{
			return _context.Products
				.Where(p => p.Id == id && !p.IsDeleted)
				.Select(p => new ProductDetailsViewModel
				{
					Id = p.Id,
					ProductName = p.ProductName,
					Description = p.Description,
					Price = p.Price,
					ImageUrl = p.ImageUrl,
					CategoryName = p.Category.Name,
					AddedOn = p.AddedOn.ToString(DateTimeFormat),
					Seller = p.Seller.UserName,
					HasBought = p.ProductsClients.Any()
				})
				.FirstOrDefaultAsync();
		}
		public async Task<bool> HasUserBoughtProductAsync(string userId, int productId)
		{
			return await _context.ProductsClients
				.AnyAsync(pc => pc.ClientId == userId && pc.ProductId == productId);
		}

		public async Task AddToCartAsync(string userId, ProductViewModel product)
		{
			bool isProductInCart = await _context.ProductsClients
				.AnyAsync(pc => pc.ClientId == userId && pc.ProductId == product.Id);

			bool hasBought = await HasUserBoughtProductAsync(userId, product.Id);

			if (!isProductInCart && !hasBought)
			{
				var userProduct = new ProductClient
				{
					ClientId = userId,
					ProductId = product.Id
				};

				await _context.ProductsClients.AddAsync(userProduct);
				await _context.SaveChangesAsync();
			}
		}

		public async Task RemoveFromCartAsync(string userId, ProductViewModel product)
		{
			var isProductInCart = await _context.ProductsClients
				.FirstOrDefaultAsync(pc => pc.ClientId == userId && pc.ProductId == product.Id);

			if (isProductInCart != null)
			{
				_context.ProductsClients.Remove(isProductInCart);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<AddProductViewModel> GetProductForAddAsync()
		{
			var categories = await _context.Categories
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.AsNoTracking()
				.ToListAsync();

			var model = new AddProductViewModel
			{
				Categories = categories
			};

			return model;
		}

		public async Task AddProductAsync(AddProductViewModel model, string userId)
		{
			Product product = new Product
			{
				ProductName = model.ProductName,
				Description = model.Description,
				Price = model.Price,
				ImageUrl = model.ImageUrl,
				AddedOn = DateTime.Parse(model.AddedOn),
				CategoryId = model.CategoryId,
				SellerId = userId
			};

			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
		}

		public async Task EditProductAsync(ProductViewModel model)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

			if (product != null)
			{
				product.ProductName = model.ProductName;
				product.Price = model.Price;
				product.Description = model.Description;
				product.ImageUrl = model.ImageUrl;
				product.AddedOn = DateTime.Parse(model.AddedOn);
				product.CategoryId = model.CategoryId;
				product.SellerId = model.SellerId;

				await _context.SaveChangesAsync();
			}
		}

		public async Task<DeleteProductViewModel?> GetProductForDeleteAsync(int id)
		{
			return await _context.Products
				.Where(p => p.Id == id)
				.Select(p => new DeleteProductViewModel
				{
					Id = p.Id,
					ProductName = p.ProductName,
					SellerId = p.SellerId,
					Seller = p.Seller.UserName
				})
				.FirstOrDefaultAsync();
		}

		public async Task SoftDeleteProductAsync(int id)
		{
			var product = await _context.Products
				.Where(p => p.Id == id)
				.FirstOrDefaultAsync();

			if (product != null)
			{
				product.IsDeleted = true;
				await _context.SaveChangesAsync();
			}
		}
	}
}