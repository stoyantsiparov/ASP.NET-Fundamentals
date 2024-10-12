using System.Reflection.Emit;
using GameZone.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
	public class GameZoneDbContext : IdentityDbContext
	{
		public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<GamerGame>()
				.HasKey(gg => new { gg.GameId, gg.GamerId });

			// Configure the relationship between Games and AspNetUsers
			builder.Entity<Game>()
				.HasOne(g => g.Publisher)
				.WithMany()
				.HasForeignKey(g => g.PublisherId)
				.OnDelete(DeleteBehavior.Cascade);

			// Configure the relationship between Games and Genres
			builder.Entity<Game>()
				.HasOne(g => g.Genre)
				.WithMany()
				.HasForeignKey(g => g.GenreId)
				.OnDelete(DeleteBehavior.Cascade);

			// Configure the relationship between GamersGames and AspNetUsers
			builder.Entity<GamerGame>()
				.HasOne(gg => gg.Gamer)
				.WithMany()
				.HasForeignKey(gg => gg.GamerId)
				.OnDelete(DeleteBehavior.Cascade);

			// Configure the relationship between GamersGames and Games
			builder.Entity<GamerGame>()
				.HasOne(gg => gg.Game)
				.WithMany()
				.HasForeignKey(gg => gg.GameId)
				.OnDelete(DeleteBehavior.Cascade);

			// Seed data
			builder.Entity<Genre>().HasData(
				new Genre { Id = 1, Name = "Action" },
				new Genre { Id = 2, Name = "Adventure" },
				new Genre { Id = 3, Name = "Fighting" },
				new Genre { Id = 4, Name = "Sports" },
				new Genre { Id = 5, Name = "Racing" },
				new Genre { Id = 6, Name = "Strategy" }
			);

			builder.Entity<Game>().HasData(
				new Game
				{
					Id = 1,
					Title = "GTA V",
					Description = "Grand Theft Auto V is a 2013 action-adventure game developed by Rockstar North and published by Rockstar Games. It is the first main entry in the Grand Theft Auto series since 2008's Grand Theft Auto IV.",
					ImageUrl = "https://upload.wikimedia.org/wikipedia/en/a/a5/Grand_Theft_Auto_V.png",
					PublisherId = "1",
					ReleasedOn = new DateTime(2013, 9, 17),
					GenreId = 1
				}
			);
		}

		public virtual DbSet<Game> Games { get; set; } = null!;
		public virtual DbSet<Genre> Genres { get; set; } = null!;
		public virtual DbSet<GamerGame> GamersGames { get; set; } = null!;
	}
}
