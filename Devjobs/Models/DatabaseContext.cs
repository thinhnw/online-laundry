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
