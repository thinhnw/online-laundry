using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    [Table("Jobs")]
    public record Job
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Location { get; init; }
        public int SalaryMin { get; init; }
        public int SalaryMax { get; init; }
        public string Status { get; init; }
        //FK
        public int CorporateId { get; init; }
        public virtual Corporate Corporate { get; init; }


    }
}
