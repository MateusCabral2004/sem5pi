using JetBrains.Annotations;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Xunit;

namespace Sempi5.Tests.Domain.PersonalData.Integration
{
    public class PersonTest
    {
        [Theory]
        [InlineData("John", "Doe", "teste@gmail.com", 987654321)]
        [InlineData("Jane", "Doe", "teste@gmail.com", 923456789)]
        public void ConstructorTest(string firstName, string lastName, string email, int phoneNumber)
        {
            var emailObj = new Email(email);
            var phoneNumberObj = new PhoneNumber(phoneNumber);
            var contactInfo = new ContactInfo(emailObj, phoneNumberObj);
            var firstNameObj = new Name(firstName);
            var lastNameObj = new Name(lastName);

            var obj = new Person(firstNameObj, lastNameObj, contactInfo);

            Assert.NotNull(obj);
            Assert.Equal(firstName, obj.FirstName.ToString());
            Assert.Equal(lastName, obj.LastName.ToString());
            Assert.Equal(email, obj.ContactInfo.email().ToString());
            Assert.Equal(phoneNumber, obj.ContactInfo.phoneNumber().phoneNumber());
        }
    }
}