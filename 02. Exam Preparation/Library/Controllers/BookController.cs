using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
	public class BookController : BaseController
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}

		public async Task<IActionResult> All()
		{
			var model = await _bookService.GetAllBooksAsync();

			return View(model);
		}

		public async Task<IActionResult> Mine()
		{
			var model = await _bookService.GetMyBooksAsync(GetUserId());

			return View(model);
		}

		public async Task<IActionResult> AddToCollection(int id)
		{
			var book = await _bookService.GetBookIdAsync(id);

			if (book == null)
			{
				return RedirectToAction("All", "Book");
			}

			var userId = GetUserId();

			await _bookService.AddBookToCollectionAsync(userId, book);

			return RedirectToAction("All", "Book");

		}

		public async Task<IActionResult> RemoveFromCollection(int id)
		{
			var book = await _bookService.GetBookIdAsync(id);

			if (book == null)
			{
				return RedirectToAction("Mine", "Book");
			}

			var userId = GetUserId();

			await _bookService.RemoveBookFromCollectionAsync(userId, book);

			return RedirectToAction("Mine", "Book");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddBookViewModel model = await _bookService.GetNewAddBookModelAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBookViewModel model)
		{
			decimal rating;

			if (!decimal.TryParse(model.Rating, out rating) || rating < 0 || rating > 10)
			{
				ModelState.AddModelError("Rating", "Rating must be a number between 0.00 and 10.00.");

				return View(model);
			}

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await _bookService.AddBookAsync(model);

			return RedirectToAction("All", "Book");
		}
	}
}
