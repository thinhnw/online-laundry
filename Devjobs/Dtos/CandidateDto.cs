using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public record CandidateDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Address { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string Phone { get; init; }
        public int UserId { get; init; }
    }
}
