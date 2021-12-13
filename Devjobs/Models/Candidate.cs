using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    [Table("Candidates")]
    public record Candidate
    {
        public int Id { get; init; }
        public string Education { get; init; }
        public int YearsOfExperience { get; init; }
        public decimal CV { get; init; }
        public int UserId { get; init; }
        public virtual User User { get; init; }

    }
}
