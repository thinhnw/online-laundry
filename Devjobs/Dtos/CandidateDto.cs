﻿using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public class CandidateDto
    {
        public int Id { get; init; }
        public string Education { get; init; }
        public int YearOfExperience { get; init; }
        public decimal CV { get; init; }
        public int UserId { get; init; }
        public virtual User User { get; init; }
    }
}
