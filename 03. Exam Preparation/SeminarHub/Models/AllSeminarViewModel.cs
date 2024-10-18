using Microsoft.AspNetCore.Identity;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
	public class AllSeminarViewModel
	{
		public int Id { get; set; }

		public string Topic { get; set; } = null!;

		public string Lecturer { get; set; } = null!;

		public string Details { get; set; } = null!;

		public string DateAndTime { get; set; } = null!;

		public int Duration { get; set; }

		public string Category { get; set; } = null!;

		public string? Organizer { get; set; }
	}
}
