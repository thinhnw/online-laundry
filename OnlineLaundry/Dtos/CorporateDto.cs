using OnlineLaundry.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry.Dtos
{
    public record CorporateDto(int Id, string Name, string About, string Logo, int UserId, string Country, string Phone);
    public record CreateCorporateDto(string Name, string About, string Logo, int UserId, string Country, string Phone,IFormFile imageFile);
    public record UpdateCorporateDto(string Name = null, string About = null, IFormFile Logo = null, string Country = null, string Phone = null);
}
