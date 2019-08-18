using System;
using System.Collections.Generic;
using System.Text;

namespace UltimateMovies.Services
{
    public interface IImageService
    {
        void CreateImage(string url);
        bool ContainsImage(string url);
        int GetImageByUrl(string url);
    }
}
