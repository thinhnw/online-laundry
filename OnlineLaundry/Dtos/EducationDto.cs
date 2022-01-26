﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Dtos
{
    public class EducationDto
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        //FK
        public int CandidateId { get; set; }
    }
    public class UpdateEducationDto
    {
        public int? Id { get; set; }
        [Required]
        public string Degree { get; set; }        
        public string FieldOfStudy { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        
    }
}
