using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltimateMovies.Models;

namespace UltimateMovies.Data
{
    public class UltimateMoviesDbContext : IdentityDbContext
    {
        public DbSet<Actor> Actors { get; set; }

        public DbSet<ActorMovie> ActorsMovies { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public UltimateMoviesDbContext(DbContextOptions<UltimateMoviesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(x => new { x.ActorId, x.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany(a => a.Movies)
                .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.Actors)
                .HasForeignKey(am => am.MovieId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
