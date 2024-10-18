using Microsoft.AspNetCore.Mvc;
using SeminarHub.Contracts;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
	public class SeminarController : BaseController
	{
		private readonly ISeminarHub _seminarHub;

		public SeminarController(ISeminarHub seminarHub)
		{
			_seminarHub = seminarHub;
		}

		public async Task<IActionResult> All()
		{
			var model = await _seminarHub.GetAllSeminarsAsync();
			return View(model);
		}

		public async Task<IActionResult> Joined()
		{
			var model = await _seminarHub.GetMySeminarsAsync(GetUserId());
			return View(model);
		}

		public async Task<IActionResult> Join(int id)
		{
			var seminar = await _seminarHub.GetSeminarIdAsync(id);

			if (seminar == null)
			{
				return RedirectToAction("Joined", "Seminar");
			}

			var userId = GetUserId();
			await _seminarHub.AddToMySeminarsAsync(userId, seminar);

			return RedirectToAction("Joined", "Seminar");
		}

		public async Task<IActionResult> Leave(int id)
		{
			var seminar = await _seminarHub.GetSeminarIdAsync(id);

			if (seminar == null)
			{
				return RedirectToAction("Joined", "Seminar");
			}

			var userId = GetUserId();
			await _seminarHub.RemoveFromMySeminarsAsync(userId, seminar);

			return RedirectToAction("Joined", "Seminar");
		} 

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			AddSeminarViewModel model = await _seminarHub.GetNewAddSeminarViewModelAsync();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddSeminarViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			var userId = GetUserId();
			await _seminarHub.AddSeminarAsync(model, userId);
			return RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var model = await _seminarHub.GetSeminarIdAsync(id);
			if (model != null)
			{
				var categories = await _seminarHub.GetNewAddSeminarViewModelAsync();
				model.Categories = categories.Categories;
				return View(model);
			}

			return RedirectToAction("All", "Seminar");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SeminarViewModel model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await _seminarHub.EditSeminarAsync(model);
			return RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var model = await _seminarHub.GetSeminarDetailsAsync(id);
			if (model != null)
			{
				return View(model);
			}
			return RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var model = await _seminarHub.GetSeminarToDeleteAsync(id);

			if (model != null)
			{
				return View(model);
			}

			return RedirectToAction("All", "Seminar");
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(DeleteSeminarViewModel model)
		{
			await _seminarHub.DeleteSeminarAsync(model.Id);

			return RedirectToAction("All", "Seminar");
		}
	}
}
