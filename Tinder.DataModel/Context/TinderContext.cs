using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tinder.DataModel.Entities;

namespace Tinder.DataModel.Context
{
    public class TinderContext : DbContext
    {
        public TinderContext(DbContextOptions<TinderContext> options) : base(options)
        {

        }
        public DbSet<User>  User { get; set; }
        public DbSet<UserLike> Like { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLike>()
                .HasKey(key => new { key.SourceUserId, key.LikedUserId });

            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
