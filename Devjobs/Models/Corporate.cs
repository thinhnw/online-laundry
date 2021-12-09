using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Models
{
    [Table("Corporates")]
    public record Corporate
    {
        public int Id { get; init; }
        public string About { get; init; }
        public int UserId { get; init; }
        public virtual User User { get; init; }
    }
}
