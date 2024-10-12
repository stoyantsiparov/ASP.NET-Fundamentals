using GameZone.Contracts;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
	public class GameController : BaseController
	{
		private readonly IGameService _gameService;

		public GameController(IGameService gameService)
		{
			_gameService = gameService;
		}

		public async Task<IActionResult> All()
		{
			var model = await _gameService.GetAllGamesAsync();

			return View(model);
		}

		public async Task<IActionResult> MyZone()
		{
			var model = await _gameService.GetMyGamesAsync(GetUserId());

			return View(model);
		}

		public async Task<IActionResult> AddToMyZone(int id)
		{
			var game = await _gameService.GetGameIdAsync(id);

			if (game == null)
			{
				return RedirectToAction("MyZone", "Game");
			}

			var userId = GetUserId();

			await _gameService.AddToMyZoneAsync(userId, game);

			return RedirectToAction("MyZone", "Game");
		}

		public async Task<IActionResult> StrikeOut(int id)
		{
			var game = await _gameService.GetGameIdAsync(id);

			if (game == null)
			{
				return RedirectToAction("MyZone", "Game");
			}

			var userId = GetUserId();

			await _gameService.StrikeOutAsync(userId, game);

			return RedirectToAction("MyZone", "Game");
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddGameViewModel model = await _gameService.GetNewAddGameViewModelAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddGameViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var userId = GetUserId();
			await _gameService.AddGameAsync(model, userId);

			return RedirectToAction("All", "Game");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await _gameService.GetGameIdAsync(id);

			if (model != null)
			{
				var genres = await _gameService.GetNewAddGameViewModelAsync();
				model.Genres = genres.Genres;

				return View(model);
			}

			return RedirectToAction("All", "Game");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(GameViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await _gameService.EditGameAsync(model);

			return RedirectToAction("All", "Game");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var model = await _gameService.GetGameDetailsAsync(id);

			if (model != null)
			{
				return View(model);
			}

			return RedirectToAction("All", "Game");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await _gameService.GetGameToDeleteAsync(id);

			if (model != null)
			{
				return View(model);
			}

			return RedirectToAction("All", "Game");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(DeleteGameViewModel model)
		{
			await _gameService.DeleteGameAsync(model.Id);

			return RedirectToAction("All", "Game");
		}
	}
}
