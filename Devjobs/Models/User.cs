﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devjobs.Models
{
    [Table("Users")]
    public record User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string Username { get; set; }               
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}