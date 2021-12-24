using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs.Dtos
{
    public record CorporateDto(int Id, string Name, string About, string Logo, int UserId);
    public record CreateCorporateDto(string Name, string About, string Logo, int UserId);
    public record UpdateCorporateDto(string Name, string About, string Logo);
}
