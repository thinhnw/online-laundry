using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    public record Education
    {
        public int Id { get; init; }
        public string Degree { get; init; }
        public string FieldOfStudy { get; init; }
        public string School { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public DateTime FromTime { get; init; }
        public DateTime ToTime { get; init; }

        //FK
        public int CandidateId { get; init; }
        public virtual Candidate Candidate { get; init; }
    }
}
