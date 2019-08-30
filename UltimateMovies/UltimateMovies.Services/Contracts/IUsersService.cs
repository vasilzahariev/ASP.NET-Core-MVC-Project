﻿using System;
using System.Collections.Generic;
using System.Text;
using UltimateMovies.Models;

namespace UltimateMovies.Services
{
    public interface IUsersService
    {
        void AddMovieToWishList(string username, int movieId);

        ICollection<Movie> GetMoviesFromWishList(string username);

        void RemoveMovieFromWishList(string username, int movieId);

        UMUser GetUser(string username);

        void AddPhoneToUser(string username, string phone);

        UMUser GetUserById(string id);

        ICollection<UMUser> GetAllUsers();

        string GetUserRole(string id);
    }
}
