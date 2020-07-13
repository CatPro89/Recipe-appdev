using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;
using System.IO;
using Xamarin.Forms;

namespace RecipeApp.Services
{
    public class RecipeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var externalStoragePath = DependencyService.Get<IExternalStorage>().GetPath();
            var databasePath = Path.Combine(externalStoragePath, Constants.DatabaseFileName);

            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}