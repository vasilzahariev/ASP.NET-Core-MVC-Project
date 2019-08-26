using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltimateMovies.Models;

namespace UltimateMovies.Data
{
    public class UltimateMoviesDbContext : IdentityDbContext<UMUser>
    {
        public DbSet<Actor> Actors { get; set; }

        public DbSet<ActorMovie> ActorsMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<WishListMovie> WishListMovies { get; set; }

        public DbSet<CartMovie> CartMovies { get; set; }

        public UltimateMoviesDbContext(DbContextOptions<UltimateMoviesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(x => new { x.ActorId, x.MovieId });
            modelBuilder.Entity<WishListMovie>().HasKey(x => new { x.MovieId, x.UserId });
            modelBuilder.Entity<CartMovie>().HasKey(x => new { x.UserId, x.MovieId });

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany(a => a.Movies)
                .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.Actors)
                .HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<WishListMovie>()
                .HasOne(wlm => wlm.Movie)
                .WithMany(a => a.WishList)
                .HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<WishListMovie>()
                .HasOne(am => am.User)
                .WithMany(m => m.WishList)
                .HasForeignKey(am => am.UserId);

            modelBuilder.Entity<CartMovie>()
                .HasOne(cm => cm.User)
                .WithMany(u => u.Cart)
                .HasForeignKey(cm => cm.UserId);

            modelBuilder.Entity<CartMovie>()
                .HasOne(cm => cm.Movie)
                .WithMany(a => a.Cart)
                .HasForeignKey(cm => cm.MovieId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
