﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Tests.Models
{
    [ExcludeFromCodeCoverage]
    public class MockServiceResults
    {
        public static object GetMessagesSuccessResult()
        {
            return new List<Message>
            {
                new()
                {
                    FirstName = "Jack",
                    LastName = "Johnson",
                    Email = "Jack@johnson.com",
                    Subject = "Hi There",
                    Detail = "Just saying hello."
                }
            };
        }
        public static object GetMessagesDtoSuccessResult()
        {
            return new List<MessageDto>
            {
                new()
                {
                    FirstName = "Jack",
                    LastName = "Johnson",
                    Email = "Jack@johnson.com",
                    Subject = "Hi There",
                    Detail = "Just saying hello."
                }
            };
        }
        public static object GetResumeSuccessResult()
        {
            return new Resume
            {
                Id = 1,
                Name = "Joe",
                Summary = "summary",
                WorkExperiences = new List<WorkExperience>
                {
                    new()
                    {
                        Id = 1,
                        CompanyName = "name",
                        CompanyCity = "Detroit",
                        CompanyState = "MI",
                        CompanyUrl = "www.company.com",
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today.AddDays(1),
                        Title = "My Title",
                        WorkDetails = new List<WorkDetail>
                        {
                            new()
                            {
                                Id = 1,
                                Detail = "detail",
                            }
                        },
                    }
                },
                Educations = new List<Education>
                {
                    new()
                    {
                        Id = 1,
                        InstitutionName = "school",
                        Credential = "MS Badness",
                        EndDate = DateTime.Today.AddDays(-365),
                        InstitutionUrl = "www.school.com",
                    }
                },
                Skills = new List<Skill>
                {
                    new()
                    {
                        Id = 1,
                        Name = "coolskill",
                        SkillType = SkillType.Business,
                        IsKeySkill = false,
                    }
                },
                Certifications = new List<Certification>
                {
                    new()
                    {
                        Id = 1,
                        Source = "cert.com",
                        SourceUrl = "www.cert.com",
                        Subject = "coolness",
                        CredentialId = "10001",
                        StartDate = default,
                        EndDate = DateTime.Today.AddDays(-730),
                    }
                }
            };
        }
    }
}
