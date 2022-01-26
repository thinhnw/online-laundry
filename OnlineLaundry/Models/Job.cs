using OnlineLaundry.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Models
{
    [Table("Jobs")]
    public record Job
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool IsRemote { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
        public int HiringNumber { get; set; }
        [Required]
        public string Type { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public string SalaryRate { get; set; }
        [Required]        
        public string Description { get; set; }
        //FK
        public int CorporateId { get; set; }
        public virtual Corporate Corporate { get; set; }
    }
}
