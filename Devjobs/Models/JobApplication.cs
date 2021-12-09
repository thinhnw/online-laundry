using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    [Table("JobApplications")]
    public record JobApplication
    {
        public int Id { get; init; }
        public string Status { get; init; }
        public string CV { get; init; }

        //FK
        public int CandidateId { get; init; }
        public virtual Candidate Candidate { get; init; }
        public int JobId { get; init; }
        public virtual Job Job { get; init; }
    }
}
