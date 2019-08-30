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

        public DbSet<Address> Addresses { get; set; }

        public DbSet<CartMovie> CartMovies { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<WishListMovie> WishListMovies { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderMovie> OrderMovies { get; set; }

        public DbSet<LibraryMovie> LibraryMovies { get; set; }

        public DbSet<SuggestedMovie> SuggestedMovies { get; set; }

        public UltimateMoviesDbContext(DbContextOptions<UltimateMoviesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>().HasKey(x => new { x.ActorId, x.MovieId });
            modelBuilder.Entity<WishListMovie>().HasKey(x => new { x.MovieId, x.UserId });
            modelBuilder.Entity<CartMovie>().HasKey(x => new { x.UserId, x.MovieId });
            modelBuilder.Entity<OrderMovie>().HasKey(x => new { x.MovieId, x.OrderId });
            modelBuilder.Entity<LibraryMovie>().HasKey(x => new { x.UserId, x.MovieId });

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

            modelBuilder.Entity<UMUser>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User);

            modelBuilder.Entity<UMUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);

            modelBuilder.Entity<OrderMovie>()
                .HasOne(om => om.Order)
                .WithMany(o => o.Movies)
                .HasForeignKey(om => om.OrderId);

            modelBuilder.Entity<LibraryMovie>()
                .HasOne(lm => lm.User)
                .WithMany(l => l.Library)
                .HasForeignKey(lm => lm.UserId);

            modelBuilder.Entity<LibraryMovie>()
                .HasOne(lm => lm.Movie)
                .WithMany(m => m.Libraries)
                .HasForeignKey(lm => lm.MovieId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
