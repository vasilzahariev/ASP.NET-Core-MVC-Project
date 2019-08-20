using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IImageService
    {
        void CreateImage(string url);

        bool ContainsImage(string url);

        int GetImageByUrl(string url);

        Image GetImage(int id);
    }
}
