using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Devjobs.Dtos;

#nullable disable

namespace Devjobs.Models
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
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobApplication> JobApplications { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        
    }
}
