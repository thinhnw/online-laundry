﻿using OnlineLaundry.Dtos;
using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLaundry
{
    public static class Extensions
    {
        public static CorporateDto AsDto(this Corporate corporate)
        {
            return new CorporateDto(corporate.Id, corporate.Name, corporate.About, corporate.Logo, corporate.UserId, corporate.Country, corporate.Phone);
        }
        public static JobDto AsDto(this Job job)
        {
            //TODO
            return new JobDto
            {
                Corporate = job.Corporate.AsDto(),
                Title = job.Title,

                Country = job.Country,
                City = job.City,
                Type = job.Type,
                HiringNumber = job.HiringNumber,
                IsRemote = job.IsRemote,

                SalaryMin = job.SalaryMin,
                SalaryMax = job.SalaryMax,
                SalaryRate = job.SalaryRate,
                Status = job.Status,
                Description = job.Description     
            };
        }
        public static JobApplicationDto AsDto(this JobApplication jobApplication)
        {
            return new JobApplicationDto
            {
                CandidateId=jobApplication.CandidateId,
                JobId=jobApplication.JobId,
                CV = jobApplication.CV,
                Status = jobApplication.Status
            };
        }
        public static CandidateDto AsDto(this Candidate candidate)
        {
            return new CandidateDto
            {
                UserId=candidate.UserId,
                City=candidate.City,
                Country=candidate.Country,
                Address=candidate.Address,
                FirstName=candidate.FirstName,
                LastName=candidate.LastName,
                Phone=candidate.Phone,
                Educations=candidate.Educations.Select(e => e.AsDto()).ToList(),
                WorkExperiences = candidate.WorkExperiences.Select(e => e.AsDto()).ToList(),
                Skills = candidate.Skills.Select(s => s.AsDto()).ToList(),
            };
        }
        public static EducationDto AsDto(this Education education)
        {
            return new EducationDto
            {
                Id = education.Id,
                CandidateId = education.CandidateId,
                City = education.City,
                Country = education.Country,
                Degree = education.Degree,
                FieldOfStudy = education.FieldOfStudy,
                FromTime = education.FromTime,
                School = education.School,
                ToTime = education.ToTime,
            };
        }
        public static WorkExperienceDto AsDto(this WorkExperience workExperience)
        {
            return new WorkExperienceDto
            {
                Id = workExperience.Id,
                CandidateId = workExperience.CandidateId,
                City = workExperience.City,
                Country = workExperience.Country,
                Description = workExperience.Description,
                FromTime = workExperience.FromTime,
                JobTitle = workExperience.JobTitle,
                Organization = workExperience.Organization,
                ToTime = workExperience.ToTime
            };
        }
        public static SkillDto AsDto(this Skill skill)
        {
            return new SkillDto
            {
                Id = skill.Id,
                CandidateId = skill.CandidateId,
                Name = skill.Name            
            };
        }

    }
}
