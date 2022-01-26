using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineLaundry.Dtos;

#nullable disable

namespace OnlineLaundry.Models
{
    public partial class DatabaseContext : DbContext
    {        
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.User)
                .WithOne(u => u.Candidate);
            modelBuilder.Entity<Corporate>()
                .HasOne(c => c.User)
                .WithOne(u => u.Corporate);
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Educations)
                .WithOne(e => e.Candidate);
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.WorkExperiences)
                .WithOne(e => e.Candidate);
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Skills)
                .WithOne(s => s.Candidate);
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
    }
}
