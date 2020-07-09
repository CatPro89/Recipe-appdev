using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;
using System;
using System.IO;

namespace RecipeApp.Services
{
    public class RecipeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.DatabaseFileName);

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}