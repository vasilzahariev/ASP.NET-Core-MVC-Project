using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UltimateMovies.Data
{
    public class UltimateMoviesDbContext : IdentityDbContext
    {
        public UltimateMoviesDbContext(DbContextOptions<UltimateMoviesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
