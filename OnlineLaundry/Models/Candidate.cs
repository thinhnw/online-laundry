using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Models
{
    [Table("Candidates")]
    public record Candidate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public int UserId { get; set; }        
        public virtual User User { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ICollection<Skill> Skills { get; set; }

    }
}
