using Microsoft.EntityFrameworkCore;
using SeminarHub.Contracts;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;

namespace SeminarHub.Services
{
	public class SeminarHubClass : ISeminarHub
	{
		private readonly SeminarHubDbContext _context;

		public SeminarHubClass(SeminarHubDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<AllSeminarViewModel>> GetAllSeminarsAsync()
		{
			return await _context.Seminars
				.Select(s => new AllSeminarViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Details = s.Details,
					DateAndTime = s.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
					Duration = s.Duration,
					Category = s.Category.Name,
					Organizer = s.Organizer.UserName
				})
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<AllSeminarViewModel>> GetMySeminarsAsync(string userId)
		{
			return await _context.SeminarsParticipants
				.Where(sp => sp.ParticipantsId == userId)
				.Select(sp => new AllSeminarViewModel
				{
					Id = sp.Seminar.Id,
					Topic = sp.Seminar.Topic,
					Lecturer = sp.Seminar.Lecturer,
					Details = sp.Seminar.Details,
					DateAndTime = sp.Seminar.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
					Duration = sp.Seminar.Duration,
					Category = sp.Seminar.Category.Name,
					Organizer = sp.Seminar.Organizer.UserName
				})
				.ToListAsync();
		}

		public async Task<SeminarViewModel?> GetSeminarIdAsync(int id)
		{
			return await _context.Seminars
				.Where(s => s.Id == id)
				.Select(s => new SeminarViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Details = s.Details,
					DateAndTime = s.DateAndTime.ToString("dd/MM/yyyy HH:mm"),
					Duration = s.Duration,
					CategoryId = s.CategoryId
				})
				.FirstOrDefaultAsync();
		}

		public async Task<SeminarDetailsViewModel?> GetSeminarDetailsAsync(int id)
		{
			return await _context.Seminars
				.Where(s => s.Id == id)
				.Select(s => new SeminarDetailsViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Details = s.Details,
					Lecturer = s.Lecturer,
					Organizer = s.Organizer.UserName,
					Category = s.Category.Name,
					Duration = s.Duration
				})
				.FirstOrDefaultAsync();
		}

		public async Task AddToMySeminarsAsync(string userId, SeminarViewModel seminar)
		{
			bool isSeminarAdded = await _context.SeminarsParticipants
				.AnyAsync(sp => sp.ParticipantsId == userId && sp.SeminarId == seminar.Id);

			if (isSeminarAdded == false)
			{
				var userSeminar = new SeminarParticipant
				{
					ParticipantsId = userId,
					SeminarId = seminar.Id
				};
				await _context.SeminarsParticipants.AddAsync(userSeminar);
				await _context.SaveChangesAsync();
			}
		}

		public async Task RemoveFromMySeminarsAsync(string userId, SeminarViewModel seminar)
		{
			var isSeminarAdded = _context.SeminarsParticipants
				.FirstOrDefault(sp => sp.ParticipantsId == userId && sp.SeminarId == seminar.Id);

			if (isSeminarAdded != null)
			{
				_context.SeminarsParticipants.Remove(isSeminarAdded);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<AddSeminarViewModel> GetNewAddSeminarViewModelAsync()
		{
			var categories = await _context.Categories
				.Select(c => new CategoryViewModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.AsNoTracking()
				.ToListAsync();

			var model = new AddSeminarViewModel()
			{
				Categories = categories
			};

			return model;
		}

		public async Task AddSeminarAsync(AddSeminarViewModel model, string userId)
		{
			Seminar seminar = new Seminar()
			{
				Topic = model.Topic,
				Details = model.Details,
				Duration = model.Duration,
				DateAndTime = DateTime.Parse(model.DateAndTime),
				Lecturer = model.Lecturer,
				CategoryId = model.CategoryId,
				OrganizerId = userId
			};

			await _context.Seminars.AddAsync(seminar);
			await _context.SaveChangesAsync();
		}

		public async Task EditSeminarAsync(SeminarViewModel model)
		{
			var seminar = await _context.Seminars.FirstOrDefaultAsync(s => s.Id == model.Id);
			if (seminar != null)
			{
				seminar.Topic = model.Topic;
				seminar.Details = model.Details;
				seminar.Duration = model.Duration;
				seminar.DateAndTime = DateTime.Parse(model.DateAndTime);
				seminar.CategoryId = model.CategoryId;
				seminar.Lecturer = model.Lecturer;

				await _context.SaveChangesAsync();
			}
		}

		public async Task<DeleteSeminarViewModel?> GetSeminarToDeleteAsync(int id)
		{
			return await _context.Seminars
				.Where(s => s.Id == id)
				.Select(s => new DeleteSeminarViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					DateAndTime = s.DateAndTime
				})
				.FirstOrDefaultAsync();
		}

		public async Task DeleteSeminarAsync(int id)
		{
			var seminar = await _context.Seminars
				.Where(s => s.Id == id)
				.FirstOrDefaultAsync();

			if (seminar != null)
			{
				_context.Seminars.Remove(seminar);
				await _context.SaveChangesAsync();
			}
		}
	}
}
