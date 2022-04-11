using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;

namespace JosephHungerman.Tests.Models
{
    [ExcludeFromCodeCoverage]
    public class MockServiceResults
    {
        public static object GetMessagesSuccessResult()
        {
            return new List<Message>
            {
                new Message
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
                new MessageDto
                {
                    FirstName = "Jack",
                    LastName = "Johnson",
                    Email = "Jack@johnson.com",
                    Subject = "Hi There",
                    Detail = "Just saying hello."
                }
            };
        }
    }
}
