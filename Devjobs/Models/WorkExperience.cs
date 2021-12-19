using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    public record WorkExperience
    {
        public int Id { get; init; }
        public string JobTitle { get; init; }
        public string Organization { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public DateTime FromTime { get; init; }
        public DateTime ToTime { get; init; }
        public string Description { get; init; }

        //FK
        public int CandidateId { get; init; }
        public virtual Candidate Candidate { get; init; }

    }
}
