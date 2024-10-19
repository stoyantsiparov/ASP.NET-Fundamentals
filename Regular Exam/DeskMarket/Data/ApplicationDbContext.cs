﻿using DeskMarket.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder
				.Entity<Category>()
				.HasData(
					new Category { Id = 1, Name = "Laptops" },
					new Category { Id = 2, Name = "Workstations" },
					new Category { Id = 3, Name = "Accessories" },
					new Category { Id = 4, Name = "Desktops" },
					new Category { Id = 5, Name = "Monitors" });

			builder.Entity<ProductClient>()
				.HasKey(pc => new { pc.ProductId, pc.ClientId });

			builder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);
		}

		public virtual DbSet<Product> Products { get; set; } = null!;
		public virtual DbSet<Category> Categories { get; set; } = null!;
		public virtual DbSet<ProductClient> ProductsClients { get; set; } = null!;
	}
}
