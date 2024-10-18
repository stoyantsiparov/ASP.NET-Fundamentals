using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Contracts;
using SoftUniBazar.Models;

namespace SoftUniBazar.Controllers
{
    public class AdController : BaseController
    {
        private readonly ISoftUniBazar _bazar;

        public AdController(ISoftUniBazar bazar)
        {
            _bazar = bazar;
        }

        public async Task<IActionResult> All()
        {
            var model = await _bazar.GetAllBazarAsync();
            return View(model);
        }

        public async Task<IActionResult> Cart()
        {
            var userId = GetUserId();
            var model = await _bazar.GetMyBazarAsync(userId);
            return View(model);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var model = await _bazar.GetBazarIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("All", "Ad");
            }
            var userId = GetUserId();
            await _bazar.AddToMyBazarAsync(userId, model);
            return RedirectToAction("Cart", "Ad");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var model = await _bazar.GetBazarIdAsync(id);
            if (model == null)
            {
                return RedirectToAction("Cart", "Ad");
            }
            var userId = GetUserId();
            await _bazar.RemoveFromMyBazarAsync(userId, model);
            return RedirectToAction("All", "Ad");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await _bazar.GetNewAdBazarViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBazarViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var userId = GetUserId();
            await _bazar.AddBazarAsync(model, userId);
            return RedirectToAction("All", "Ad");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _bazar.GetBazarIdAsync(id);
            if (model != null)
            {
                var categories = await _bazar.GetNewAdBazarViewModelAsync();
                model.Categories = categories.Categories;
                return View(model);
            }
            return RedirectToAction("All", "Ad");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BazarViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            await _bazar.EditBazarAsync(model);
            return RedirectToAction("All", "Ad");
        }
    }
}
