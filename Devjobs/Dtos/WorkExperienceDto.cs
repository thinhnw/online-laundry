using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class WorkExperienceDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Organization { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string Description { get; set; }

        //FK
        public int CandidateId { get; set; }
    }

    public class UpdateWorkExperienceDto
    {
        public int? Id { get; set; }
        public string JobTitle { get; set; }        
        public string Organization { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

    }
}
