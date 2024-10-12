using GameZone.Models;

namespace GameZone.Contracts
{
	public interface IGameService
	{
		Task<IEnumerable<AllGameViewModel>> GetAllGamesAsync();
		Task<IEnumerable<AllGameViewModel>> GetMyGamesAsync(string userId);
		Task<GameViewModel?> GetGameIdAsync(int id);
		Task<GameDetailsViewModel?> GetGameDetailsAsync(int id);
		Task AddToMyZoneAsync(string userId, GameViewModel game);
		Task StrikeOutAsync(string userId, GameViewModel game);
		Task<AddGameViewModel> GetNewAddGameViewModelAsync();
		Task AddGameAsync(AddGameViewModel model, string userId);
		Task EditGameAsync(GameViewModel model);
		Task<DeleteGameViewModel?> GetGameToDeleteAsync(int id);
		Task DeleteGameAsync(int id);
	}
}
