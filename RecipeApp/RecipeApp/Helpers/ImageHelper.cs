using System;
using System.IO;
using Xamarin.Forms;

namespace RecipeApp.Helpers
{
    public static class ImageHelper
    {
        public static ImageSource GetImageSource(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return ImageSource.FromResource(Constants.Resource_NoImage);

            return ImageSource.FromFile(imagePath);
        }

        /// <summary>
        /// Copies an image file to the apps pictures folder and returns the path of the copied image
        /// </summary>
        /// <param name="sourcePath">Source path of the image</param>
        /// <returns>Path of the copied image</returns>
        public static string CopyImage(string sourcePath)
        {
            var extension = Path.GetExtension(sourcePath);

            var imagePath = GenerateImagePath(extension);

            AssertDirectoryExists(imagePath);

            File.Copy(sourcePath, imagePath);

            return imagePath;
        }

        private static string GenerateImagePath(string extension)
        {
            var imagePath = GetImagePath();
            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmssffffff}{extension}";

            return Path.Combine(imagePath, fileName);
        }

        private static string GetImagePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Pictures");
        }

        private static void AssertDirectoryExists(string imagePath)
        {
            string directory = Path.GetDirectoryName(imagePath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
    }
}