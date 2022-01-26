using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Dtos
{
    public record CandidateDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        
        public ICollection<EducationDto> Educations { get; set; }
        public ICollection<WorkExperienceDto> WorkExperiences { get; set; }
        public ICollection<SkillDto> Skills { get; set; }
    }

    public class CandidatePersonalDetailsDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        
        public string Address { get; set; }
        public string Phone { get; set; }      
    }
}
