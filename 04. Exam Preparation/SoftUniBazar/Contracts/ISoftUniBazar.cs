using SoftUniBazar.Models;

namespace SoftUniBazar.Contracts
{
    public interface ISoftUniBazar
    {
        Task<IEnumerable<AllBazarViewModel>> GetAllBazarAsync();
        Task<IEnumerable<AllBazarViewModel>> GetMyBazarAsync(string userId);

        Task<BazarViewModel?> GetBazarIdAsync(int id);
        Task AddToMyBazarAsync(string userId, BazarViewModel bazar);
        Task RemoveFromMyBazarAsync(string userId, BazarViewModel bazar);
        Task<AddBazarViewModel> GetNewAdBazarViewModelAsync();
        Task AddBazarAsync(AddBazarViewModel model, string userId);
        Task EditBazarAsync(BazarViewModel model);
    }
}
