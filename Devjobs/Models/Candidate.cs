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
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string Phone { get; init; }

        public int UserId { get; init; }        
        public virtual User User { get; init; }

    }
}
