using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateMovies.Data;
using UltimateMovies.Models;
using UltimateMovies.Models.Enums;
using Xunit;

namespace UltimateMovies.Services.Tests
{
    public class ActorsServiceTests
    {
        [Fact]
        public void CreateActorShouldCreateActor()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Actors_CreateActor_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IActorsService actorsService = new ActorsService(db);

            actorsService.CreateActor("Test Actor");
            actorsService.CreateActor("Test Actor 2");
            actorsService.CreateActor("Test Actor 3");

            int actorsCount = db.Actors.ToList().Count();

            Assert.Equal(3, actorsCount);
        }

        [Fact]
        public void GetActorShouldReturnActor()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Actors_GetActor_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IActorsService actorsService = new ActorsService(db);

            db.Actors.Add(new Actor
            {
                Name = "Mark Hamill"
            });

            db.SaveChanges();

            Actor actor = actorsService.GetActor(db.Actors.Last().Id);

            Assert.Equal("Mark Hamill", actor.Name);
        }

        [Fact]
        public void GetActorsMoviesAndPostersShouldReturnAListOfMovies()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Actors_GetActorsMoviesAndPosters_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IActorsService actorsService = new ActorsService(db);

            db.Movies.Add(new Movie
            {
                Name = "Test Name",
                PosterUrl = "https://imdb.com/"
            });

            db.SaveChanges();

            db.Actors.Add(new Actor
            {
                Name = "Test Actor"
            });

            db.SaveChanges();

            db.ActorsMovies.Add(new ActorMovie
            {
                ActorId = db.Actors.Last().Id,
                MovieId = db.Movies.Last().Id
            });

            db.SaveChanges();

            List<Movie> movies = actorsService.GetActorsMoviesAndPosters(db.Actors.Last().Id).ToList();

            int moviesCount = movies.Count();

            Assert.Equal(1, moviesCount);
            Assert.Equal(db.Movies.Last().Id, movies[0].Id);
            Assert.Equal("Test Name", movies[0].Name);
            Assert.Equal("https://imdb.com/", movies[0].PosterUrl);
        }

        [Fact]
        public void GetAllActorsShouldReturnAllActors()
        {
            DbContextOptions<UltimateMoviesDbContext> options = new DbContextOptionsBuilder<UltimateMoviesDbContext>()
                    .UseInMemoryDatabase(databaseName: "Actors_GetAllActors_Database")
                    .Options;
            UltimateMoviesDbContext db = new UltimateMoviesDbContext(options);

            IActorsService actorsService = new ActorsService(db);

            actorsService.CreateActor("Name 1");
            actorsService.CreateActor("Name 2");
            actorsService.CreateActor("Name 3");
            actorsService.CreateActor("Name 4");

            List<Actor> actors = actorsService.GetAllActors().ToList();

            int actorsCount = actors.Count();

            Assert.Equal(4, actorsCount);
        }
    }
}
