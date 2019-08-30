using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using Xunit;

namespace UltimateMovies.Services.Tests
{
    public class MoviesServiceTests
    {
        [Fact]
        public void CreateMovieShouldAddMoviesToTheDatabase()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                   .UseInMemoryDatabase(databaseName: "Movies_CreateMovie_Database")
                   .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            moviesService.CreateMovie("Test 1", 10, 10, 10, "Description", "Directors", MovieGenre.Action,
                                      MovieGenre.Adventure, null, DateTime.Now, 100, 10, 100, "link", "Actor Name\r\nAnother Actor", "posterUrl",
                                      "trailerUrl");

            int moviesCount = db.Movies.ToList().Count();
            int actorsCount = db.Actors.ToList().Count();

            Assert.Equal(1, moviesCount);
            Assert.Equal(2, actorsCount);
        }

        [Fact]
        public void GetMovieShouldReturnMovie()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                   .UseInMemoryDatabase(databaseName: "Movies_GetMovie_Database")
                   .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.Movies.Add(new Movie { Name = "Test Movie" });

            db.SaveChanges();

            Movie movie = moviesService.GetMovie(db.Movies.Last().Id);

            Assert.Equal("Test Movie", movie.Name);
        }

        [Fact]
        public void GetActorsNamesShouldReturnACollectionOfActors()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                   .UseInMemoryDatabase(databaseName: "Movies_GetActorsNames_Database")
                   .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.Actors.Add(new Actor
            {
                Name = "Tester"
            });

            db.Actors.Add(new Actor
            {
                Name = "Tester 2"
            });

            db.SaveChanges();

            db.ActorsMovies.Add(new ActorMovie
            {
                ActorId = db.Actors.ToList()[0].Id,
                MovieId = 1
            });

            db.ActorsMovies.Add(new ActorMovie
            {
                ActorId = db.Actors.ToList()[1].Id,
                MovieId = 1
            });

            db.SaveChanges();

            List<Actor> actors = moviesService.GetActorsNames(1).ToList();

            int actorsCount = actors.Count();

            Assert.Equal(2, actorsCount);
        }

        [Fact]
        public void GetAllMoviesShouldReturnIEnurableOfMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Movies_GetAllMovies_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.Movies.Add(new Movie { Name = "Test 1" });
            db.Movies.Add(new Movie { Name = "Test 2" });
            db.Movies.Add(new Movie { Name = "Test 3" });

            db.SaveChanges();

            IEnumerable<Movie> movies = moviesService.GetAllMovies();

            int moviesCount = movies.Count();

            Assert.Equal(3, moviesCount);
        }

        [Fact]
        public void IsMovieInUserWishListShouldReturnBoolean()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Movies_IsMovieInUserWishList_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.Users.Add(new UMUser { UserName = "Tester" });

            db.SaveChanges();

            db.Movies.Add(new Movie { Name = "Test 1" });
            db.Movies.Add(new Movie { Name = "Test 2" });

            db.SaveChanges();

            db.WishListMovies.Add(new WishListMovie
            {
                MovieId = db.Movies.ToList()[0].Id,
                UserId = db.Users.Last().Id
            });

            db.SaveChanges();

            string movie1 = moviesService.IsMovieInUserWishList("Tester", db.Movies.ToList()[0].Id) ? "In" : "Out";
            string movie2 = moviesService.IsMovieInUserWishList("Tester", db.Movies.ToList()[1].Id) ? "In" : "Out";

            Assert.Equal("In", movie1);
            Assert.Equal("Out", movie2);
        }

        [Fact]
        public void SuggestAMovieShouldAddSuggestedMovieToDatabase()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Movies_SuggestAMovie_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            moviesService.SuggestAMovie("Test", "TestUrl");

            int suggestedMoviesCount = db.SuggestedMovies.Count();

            Assert.Equal(1, suggestedMoviesCount);
        }

        [Fact]
        public void EditAMovieShouldReplaceTheOldInformationWithNewInTheDB()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Movies_EditAMovie_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.Movies.Add(new Movie
            {
                BluRayPrice = 1,
                Description = "1",
                Directors = "1",
                DvdPrice = 1,
                Genre = MovieGenre.Action,
                Genre2 = MovieGenre.Adventure,
                IMDbScore = 1,
                IMDbUrl = "1",
                Length = 1,
                Name = "",
                OnlinePrice = 1,
                PosterUrl = "1",
                RottenTomatoes = 1,
                ReleaseDate = DateTime.Now,
                TrailerUrl = "1"
            });

            db.SaveChanges();

            moviesService.EditAMovie(db.Movies.Last().Id, "2", 2, 2, 2, "2", "2", MovieGenre.Adventure, MovieGenre.Comedy, MovieGenre.Drama, DateTime.Now, 2, 2, 2, "2", "2", "2");

            Movie movie = db.Movies.Last();

            Assert.Equal("2", movie.Name);
            Assert.Equal(2, movie.BluRayPrice);
            Assert.Equal(2, movie.OnlinePrice);
            Assert.Equal(2, movie.DvdPrice);
            Assert.Equal(2, movie.IMDbScore);
            Assert.Equal(2, movie.RottenTomatoes);
            Assert.Equal("2", movie.IMDbUrl);
            Assert.Equal(2, movie.Length);
            Assert.Equal("2", movie.Description);
        }

        [Fact]
        public void RemoveActorFromMovieShouldDeleteActorMovie()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                  .UseInMemoryDatabase(databaseName: "Movies_RemoveActorFromMovie_Database")
                  .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IMoviesService moviesService = new MoviesService(db);

            db.ActorsMovies.Add(new ActorMovie { ActorId = 1, MovieId = 1 });

            db.SaveChanges();

            moviesService.RemoveActorFromMovie(1, 1);

            int count = db.ActorsMovies.Count();

            Assert.Equal(0, count);
        }
    }
}
