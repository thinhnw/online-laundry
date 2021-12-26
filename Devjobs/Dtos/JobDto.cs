using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class JobDto
    {        
        public string Title { get; set; }     
        public string Status { get; set; }        
        public bool IsRemote { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public int HiringNumber { get; set; }        
        public string Type { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public string SalaryRate { get; set; }        
        public string Description { get; set; }

        public CorporateDto Corporate { get; set; }
    }
    public class PostJobDto
    {       
        [Required]
        public string Title { get; set; }       
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
    }
}
