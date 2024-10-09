using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
	public class BookService : IBookService
	{
		private readonly LibraryDbContext _dbContext;

		public BookService(LibraryDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
		{
			return await _dbContext
				.Books
				.Select(b => new AllBookViewModel
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ImageUrl = b.ImageUrl,
					Rating = b.Rating,
					Category = b.Category.Name
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string userId)
		{
			return await _dbContext.UsersBooks
				.Where(ub => ub.CollectorId == userId)
				.Select(b => new AllBookViewModel
				{
					Id = b.Book.Id,
					Title = b.Book.Title,
					Author = b.Book.Author,
					ImageUrl = b.Book.ImageUrl,
					Description = b.Book.Description,
					Category = b.Book.Category.Name
				})
				.ToListAsync();
		}

		public async Task<BookViewModel?> GetBookIdAsync(int id)
		{
			return await _dbContext.Books
				.Where(b => b.Id == id)
				.Select(b => new BookViewModel
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ImageUrl = b.ImageUrl,
					Rating = b.Rating,
					Description = b.Description,
					CategoryId = b.CategoryId
				})
				.FirstOrDefaultAsync();
		}

		public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
		{
			bool isBookInCollection = await _dbContext.UsersBooks
				.AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

			if (isBookInCollection == false)
			{
				var userBook = new IdentityUserBook
				{
					CollectorId = userId,
					BookId = book.Id
				};

				await _dbContext.UsersBooks.AddAsync(userBook);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task RemoveBookFromCollectionAsync(string userId, BookViewModel book)
		{
			var userBook = await _dbContext.UsersBooks
				.FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

			if (userBook != null)
			{
				_dbContext.UsersBooks.Remove(userBook);
				await _dbContext.SaveChangesAsync();
			}
		}

		public async Task<AddBookViewModel> GetNewAddBookModelAsync()
		{
			var categories = await _dbContext.Categories
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();

			var model = new AddBookViewModel
			{
				Categories = categories
			};

			return model;
		}

		public async Task AddBookAsync(AddBookViewModel model)
		{
			Book book = new Book
			{
				Title = model.Title,
				Author = model.Author,
				ImageUrl = model.Url,
				Rating = decimal.Parse(model.Rating),
				Description = model.Description,
				CategoryId = model.CategoryId
			};

			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync();
		}
	}
}