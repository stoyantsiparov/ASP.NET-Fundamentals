using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using static SoftUniBazar.Common.ModelConstants;

namespace SoftUniBazar.Services
{
    public class SoftUniBazarService : ISoftUniBazar
    {
        private readonly BazarDbContext _dbContext;

        public SoftUniBazarService(BazarDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<AllBazarViewModel>> GetAllBazarAsync()
        {
            return await _dbContext.Ads
                .Select(a => new AllBazarViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString(AdCreatedOn),
                    Category = a.Category.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<AllBazarViewModel>> GetMyBazarAsync(string userId)
        {
            return await _dbContext.AdsBuyers
                .Select(ab => new AllBazarViewModel
                {
                    Id = ab.Ad.Id,
                    Name = ab.Ad.Name,
                    ImageUrl = ab.Ad.ImageUrl,
                    CreatedOn = ab.Ad.CreatedOn.ToString(AdCreatedOn),
                    Category = ab.Ad.Category.Name,
                    Description = ab.Ad.Description,
                    Price = ab.Ad.Price,
                    Owner = ab.Ad.Owner.UserName
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BazarViewModel?> GetBazarIdAsync(int id)
        {
            return await _dbContext.Ads
                .Where(a => a.Id == id)
                .Select(a => new BazarViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl = a.ImageUrl,
                    CategoryId = a.CategoryId,
                    Description = a.Description,
                    Price = a.Price
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddToMyBazarAsync(string userId, BazarViewModel bazar)
        {
            bool isBazarAdded = await _dbContext.AdsBuyers
                .AnyAsync(ab => ab.BuyerId == userId && ab.AdId == bazar.Id);
            if (isBazarAdded == false)
            {
                var userBazar = new AdBuyer
                {
                    BuyerId = userId,
                    AdId = bazar.Id
                };
                await _dbContext.AdsBuyers.AddAsync(userBazar);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveFromMyBazarAsync(string userId, BazarViewModel bazar)
        {
            var isBazarAdded = await _dbContext.AdsBuyers
                .FirstOrDefaultAsync(ab => ab.BuyerId == userId && ab.AdId == bazar.Id);
            if (isBazarAdded != null)
            {
                _dbContext.AdsBuyers.Remove(isBazarAdded);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AddBazarViewModel> GetNewAdBazarViewModelAsync()
        {
            var categories = await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .AsNoTracking()
                .ToListAsync();

            var model = new AddBazarViewModel
            {
                Categories = categories,
            };
            return model;
        }

        public async Task AddBazarAsync(AddBazarViewModel model, string userId)
        {
            Ad ad = new Ad
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                OwnerId = userId,
                CreatedOn = DateTime.Parse(model.CreatedOn)
            };
            await _dbContext.Ads.AddAsync(ad);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditBazarAsync(BazarViewModel model)
        {
            var bazar = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (bazar != null)
            {
                bazar.Name = model.Name;
                bazar.Description = model.Description;
                bazar.ImageUrl = model.ImageUrl;
                bazar.Price = model.Price;
                bazar.CategoryId = model.CategoryId;

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
