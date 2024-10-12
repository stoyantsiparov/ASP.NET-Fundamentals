using GameZone.Contracts;
using GameZone.Data;
using GameZone.Data.Models;
using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
	public class GameService : IGameService
	{
		private readonly GameZoneDbContext _context;

		public GameService(GameZoneDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AllGameViewModel>> GetAllGamesAsync()
		{
			return await _context.Games
				.Select(g => new AllGameViewModel
				{
					Id = g.Id,
					Title = g.Title,
					ImageUrl = g.ImageUrl!,
					Publisher = g.Publisher.UserName,
					ReleasedOn = g.ReleasedOn.ToString("dd/MM/yyyy"),
					Genre = g.Genre.Name,
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<AllGameViewModel>> GetMyGamesAsync(string userId)
		{
			return await _context.GamersGames
				.Where(gg => gg.GamerId == userId)
				.Select(gg => new AllGameViewModel
				{
					Id = gg.Game.Id,
					Title = gg.Game.Title,
					ImageUrl = gg.Game.ImageUrl!,
					Publisher = gg.Game.Publisher.UserName,
					ReleasedOn = gg.Game.ReleasedOn.ToString("dd/MM/yyyy"),
					Genre = gg.Game.Genre.Name,
				})
				.ToListAsync();
		}

		public async Task<GameViewModel?> GetGameIdAsync(int id)
		{
			return await _context.Games
				.Where(g => g.Id == id)
				.Select(g => new GameViewModel
				{
					Id = g.Id,
					Title = g.Title,
					ImageUrl = g.ImageUrl!,
					Description = g.Description,
					ReleasedOn = g.ReleasedOn.ToString(),
					GenreId = g.GenreId
				})
				.FirstOrDefaultAsync();
		}

		public Task<GameDetailsViewModel?> GetGameDetailsAsync(int id)
		{
			return _context.Games
				.Where(g => g.Id == id)
				.Select(g => new GameDetailsViewModel
				{
					Id = g.Id,
					Title = g.Title,
					ImageUrl = g.ImageUrl!,
					Description = g.Description,
					ReleasedOn = g.ReleasedOn.ToString("dd/MM/yyyy"),
					Genre = g.Genre.Name,
					Publisher = g.Publisher.UserName
				})
				.FirstOrDefaultAsync();
		}

		public async Task AddToMyZoneAsync(string userId, GameViewModel game)
		{
			bool isGameInMyZone = await _context.GamersGames
				.AnyAsync(gg => gg.GamerId == userId && gg.GameId == game.Id);

			if (isGameInMyZone == false)
			{
				var userGame = new GamerGame
				{
					GamerId = userId,
					GameId = game.Id
				};

				await _context.GamersGames.AddAsync(userGame);
				await _context.SaveChangesAsync();
			}
		}

		public async Task StrikeOutAsync(string userId, GameViewModel game)
		{
			var isGameInMyZone = await _context.GamersGames
				.FirstOrDefaultAsync(gg => gg.GamerId == userId && gg.GameId == game.Id);

			if (isGameInMyZone != null)
			{
				_context.GamersGames.Remove(isGameInMyZone);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<AddGameViewModel> GetNewAddGameViewModelAsync()
		{
			var genres = await _context.Genres
				.Select(g => new GenreViewModel
				{
					Id = g.Id,
					Name = g.Name
				})
				.AsNoTracking()
				.ToListAsync();

			var model = new AddGameViewModel
			{
				Genres = genres
			};

			return model;
		}

		public async Task AddGameAsync(AddGameViewModel model, string userId)
		{
			Game game = new Game
			{
				Description = model.Description,
				GenreId = model.GenreId,
				ImageUrl = model.ImageUrl,
				PublisherId = userId,
				ReleasedOn = DateTime.Parse(model.ReleasedOn),
				Title = model.Title
			};

			await _context.Games.AddAsync(game);
			await _context.SaveChangesAsync();
		}

		public async Task EditGameAsync(GameViewModel model)
		{
			var game = await _context.Games
				.FirstOrDefaultAsync(g => g.Id == model.Id);

			if (game != null)
			{
				game.Description = model.Description;
				game.GenreId = model.GenreId;
				game.ImageUrl = model.ImageUrl;
				game.ReleasedOn = DateTime.Parse(model.ReleasedOn);
				game.Title = model.Title;

				await _context.SaveChangesAsync();
			}
		}

		public async Task<DeleteGameViewModel?> GetGameToDeleteAsync(int id)
		{
			return await _context.Games
				.Where(g => g.Id == id)
				.Select(g => new DeleteGameViewModel
				{
					Id = g.Id,
					Title = g.Title,
					Publisher = g.Publisher.UserName
				})
				.FirstOrDefaultAsync();
		}

		public async Task DeleteGameAsync(int id)
		{
			var game = await _context.Games
				.Where(g => g.Id == id)
				.FirstOrDefaultAsync();

			if (game != null)
			{
				_context.Games.Remove(game);
				await _context.SaveChangesAsync();
			}
		}
	}
}
