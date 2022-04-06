using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Models;

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
    }
}
