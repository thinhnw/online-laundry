using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }     
        public string Role { get; set; }
        public virtual CandidateDto Candidate { get; set; }
        public virtual CorporateDto Corporate { get; set; }
    }
}
