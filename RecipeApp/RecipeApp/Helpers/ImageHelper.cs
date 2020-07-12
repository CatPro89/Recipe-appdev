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

        /// <summary>
        /// Loads an image file and converts it to a base64 encoded string
        /// </summary>
        /// <param name="imagePath">Path of the image</param>
        /// <returns>Base64 encoded image</returns>
        public static string LoadBase64EncodedImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;

            var image = File.ReadAllBytes(imagePath);

            return Convert.ToBase64String(image);
        }

        public static string GetExtension(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;

            return Path.GetExtension(imagePath);
        }

        /// <summary>
        /// Decodes a base64 encoded image, saves it to the apps pictures folder and returns the path of the saved image
        /// </summary>
        /// <param name="base64EncodedImage">Base 64 encoded image</param>
        /// <param name="extension">Image file extension</param>
        /// <returns>Path of the saved image</returns>
        public static string SaveBase64EncodedImage(string base64EncodedImage, string extension)
        {
            if (string.IsNullOrEmpty(base64EncodedImage))
                return null;

            var image = Convert.FromBase64String(base64EncodedImage);

            var imagePath = GenerateImagePath(extension);

            AssertDirectoryExists(imagePath);

            File.WriteAllBytes(imagePath, image);

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