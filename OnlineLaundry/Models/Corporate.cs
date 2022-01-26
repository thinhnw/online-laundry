using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Models
{
    [Table("Corporates")]
    public record Corporate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Logo { get; set; }

        public string Country { get; set; }
        public string Phone { get; set; }


        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
