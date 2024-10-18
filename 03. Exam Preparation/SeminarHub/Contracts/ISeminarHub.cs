using SeminarHub.Models;

namespace SeminarHub.Contracts
{
	public interface ISeminarHub
	{
		Task<IEnumerable<AllSeminarViewModel>> GetAllSeminarsAsync();
		Task<IEnumerable<AllSeminarViewModel>> GetMySeminarsAsync(string userId);
		Task<SeminarViewModel?> GetSeminarIdAsync(int id);
		Task<SeminarDetailsViewModel?> GetSeminarDetailsAsync(int id);
		Task AddToMySeminarsAsync(string userId, SeminarViewModel seminar);
		Task RemoveFromMySeminarsAsync(string userId, SeminarViewModel seminar);
		Task<AddSeminarViewModel> GetNewAddSeminarViewModelAsync();
		Task AddSeminarAsync(AddSeminarViewModel model, string userId);
		Task EditSeminarAsync(SeminarViewModel model);
		Task<DeleteSeminarViewModel?> GetSeminarToDeleteAsync(int id);
		Task DeleteSeminarAsync(int id);
	}
}
