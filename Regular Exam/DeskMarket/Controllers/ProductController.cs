using DeskMarket.Contracts;
using DeskMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskMarket.Controllers
{
	public class ProductController : BaseController
	{
		private readonly IDeskMarket _deskMarket;

		public ProductController(IDeskMarket deskMarket)
		{
			_deskMarket = deskMarket;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var userId = GetUserId();
			var model = await _deskMarket.GetAllProductsAsync(userId);

			return View(model);
		}

		public async Task<IActionResult> Cart()
		{
			var userId = GetUserId();
			var model = await _deskMarket.GetMyProductsAsync(userId);

			return View(model);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var model = await _deskMarket.GetProductDetailsAsync(id);

			if (model == null)
			{
				return RedirectToAction("Index", "Product");
			}

			return View(model);
		}

		public async Task<IActionResult> AddToCart(int id)
		{
			var product = await _deskMarket.GetProductIdAsync(id);

			if (product == null)
			{
				return RedirectToAction("Cart", "Product");
			}

			var userId = GetUserId();
			bool hasBought = await _deskMarket.HasUserBoughtProductAsync(userId, id);

			if (hasBought == false)
			{
				await _deskMarket.AddToCartAsync(userId, product);
			}

			return RedirectToAction("Index", "Product");
		}

		public async Task<IActionResult> RemoveFromCart(int id)
		{
			var product = await _deskMarket.GetProductIdAsync(id);

			if (product == null)
			{
				return RedirectToAction("Cart", "Product");
			}

			var userId = GetUserId();
			await _deskMarket.RemoveFromCartAsync(userId, product);

			return RedirectToAction("Cart", "Product");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = await _deskMarket.GetProductForAddAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddProductViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				var categories = await _deskMarket.GetProductForAddAsync();
				model.Categories = categories.Categories;
				return View(model);
			}

			var userId = GetUserId();
			await _deskMarket.AddProductAsync(model, userId);

			return RedirectToAction("Index", "Product");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await _deskMarket.GetProductIdAsync(id);

			if (model != null)
			{
				var categories = await _deskMarket.GetProductForAddAsync();
				model.Categories = categories.Categories;
				return View(model);
			}

			return RedirectToAction("Index", "Product");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(ProductViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				var categories = await _deskMarket.GetProductForAddAsync();
				model.Categories = categories.Categories;
				return View(model);
			}

			await _deskMarket.EditProductAsync(model);

			return RedirectToAction("Details", new { id = model.Id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await _deskMarket.GetProductForDeleteAsync(id);

			if (model != null)
			{
				return View(model);
			}

			return RedirectToAction("Index", "Product");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(DeleteProductViewModel model)
		{
			await _deskMarket.SoftDeleteProductAsync(model.Id);

			return RedirectToAction("Index", "Product");
		}
	}
}