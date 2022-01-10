using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    [Table("Skills")]
    public record Skill
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //FK
        [Required]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
