using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BlazorDBContext : DbContext
    {
        public BlazorDBContext(DbContextOptions<BlazorDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            }
            );

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.Property(u => u.Title).IsRequired().HasMaxLength(100);
            }
          );
        }
    }
}
