using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public class ImageService : IImageService
    {
        private UltimateMoviesDbContext db;

        public ImageService(UltimateMoviesDbContext db)
        {
            this.db = db;
        }

        public bool ContainsImage(string url)
        {
            foreach (var image in this.db.Images)
            {
                if (image.ImageUrl == url)
                {
                    return true;
                }
            }

            return false;
        }

        public void CreateImage(string url)
        {
            Image image = new Image
            {
                ImageUrl = url
            };

            this.db.Images.Add(image);

            this.db.SaveChanges();
        }

        public int GetImageByUrl(string url)
        {
            foreach (var image in this.db.Images)
            {
                if (image.ImageUrl == url)
                {
                    return image.Id;
                }
            }

            this.CreateImage(url);

            return this.ImageCount();
        }

        private int ImageCount()
        {
            int count = 0;

            foreach (var image in this.db.Images)
            {
                count++;
            }

            return count;
        }
    }
}
