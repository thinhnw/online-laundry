using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Dtos
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //FK
        public int CandidateId { get; set; }
    }

    public class UpdateSkillDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }                
    }
}
