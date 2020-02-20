using AutoMapProblem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapProblem
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
        }

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("");
            optionsBuilder.UseInMemoryDatabase("Test");
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategoryTopic> CategoryTopics { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.ProjectId });
                //entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p!.Categories)
                    .HasForeignKey(d => new { d.ParentCategoryId, d.ProjectId })
                    //.HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<CategoryTopic>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CategoryId, e.TopicId });
                //entity.HasKey(e => e.TopicId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.TopicId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p!.CategoryTopics)
                    .HasForeignKey(d => new { d.CategoryId, d.ProjectId })
                    //.HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryTopic_Category");
            });
        }
    }
}
