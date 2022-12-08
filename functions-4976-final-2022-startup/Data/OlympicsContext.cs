using System;
using System.Collections.Generic;
using AzureFunc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace AzureFunc.Data
{
    public partial class OlympicsContext : DbContext
    {
        public OlympicsContext() { }

        public OlympicsContext(DbContextOptions<OlympicsContext> options) : base(options) { }

        public OlympicsContext(DbContextOptions options) : base(options) { }


        public virtual DbSet<Game> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Country).IsRequired();
                entity.Property(e => e.Continent).IsRequired();
                entity.ToTable("Game");
            });

            builder.Entity<Game>().HasData(Helper.GetGames());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Helper.GetSetting("MYSQL_DATABASE_CONNECTION_STRING"), new MySqlServerVersion(new Version(8, 0, 29)));
                // optionsBuilder.UseSqlServer(Helper.GetSetting("SQL-SERVER_DATABASE_CONNECTION_STRING"));
            }
        }
    }
}
