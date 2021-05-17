using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    // data context class with all tables dbset
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserFavouriteMovie> UserFavouriteMovies { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFollower>()                                            //  1.
        .HasKey(k => new { k.FollowersId, k.FollowedToId });

            builder.Entity<UserFollower>()                                            //  2.
                .HasOne(u => u.FollowedTo)
                .WithMany(u => u.Followers)
                .HasForeignKey(u => u.FollowedToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollower>()                                            //  3.
                .HasOne(u => u.Followers)
                .WithMany(u => u.FollowedTo)
                .HasForeignKey(u => u.FollowersId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }

    }
}
