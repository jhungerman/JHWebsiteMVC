using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Tests.Models
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

        public static object GetSectionsSuccessResult()
        {
            return new List<Section>
            {
                new()
                {
                    Id = 1,
                    Title = "section1",
                    Paragraphs = new List<Paragraph>
                    {
                        new()
                        {
                            Id = 1,
                            Content = "content"
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Title = "section2",
                    Paragraphs = new List<Paragraph>
                    {
                        new()
                        {
                            Id = 2,
                            Content = "content"
                        }
                    }
                }
            };
        }

        public static object GetParagraphsSuccessResult()
        {
            return new List<Paragraph>
            {
                new()
                {
                    Id = 1,
                    Content = "content 1",
                },
                new()
                {
                    Id = 2,
                    Content = "content 2",
                }
            };
        }

        public static object GetQuoteSuccessResult()
        {
            return new Quote
            {
                Id = 1,
                PageType = PageType.Home,
                Text = "testtext",
                Author = "randomauthor",
                CitationUrl = "https://www.quote.com"
            };
        }
    }
}
