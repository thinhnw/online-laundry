using Devjobs.Dtos;
using Devjobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devjobs
{
    public static class Extensions
    {
        public static CorporateDto AsDto(this Corporate corporate)
        {
            return new CorporateDto(corporate.Id, corporate.Name, corporate.About, corporate.UserId);
        }
        public static JobDto AsDto(this Job job)
        {
            //TODO
            return null;
        }
    }
}
