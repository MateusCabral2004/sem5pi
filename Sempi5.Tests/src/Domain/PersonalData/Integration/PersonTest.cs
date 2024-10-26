using System;
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


        [Theory]
        [InlineData("Doe", "teste@gmail.com", 987654321)]
        [InlineData("Doe", "teste@gmail.com", 923456789)]
        public void ConstructorWithInvalidFirstNameTest(string lastName, string email, int phoneNumber)
        {
            var emailObj = new Email(email);
            var phoneNumberObj = new PhoneNumber(phoneNumber);
            var contactInfo = new ContactInfo(emailObj, phoneNumberObj);
            var lastNameObj = new Name(lastName);

            Assert.Throws<ArgumentNullException>(() => new Person(null, lastNameObj, contactInfo));
        }

        [Theory]
        [InlineData("Doe", "teste@gmail.com", 987654321)]
        [InlineData("Doe", "teste@gmail.com", 923456789)]
        public void ConstructorWithInvalidLastNameTest(string first, string email, int phoneNumber)
        {
            var firstNameObj = new Name(first);
            var emailObj = new Email(email);
            var phoneNumberObj = new PhoneNumber(phoneNumber);
            var contactInfo = new ContactInfo(emailObj, phoneNumberObj);

            Assert.Throws<ArgumentNullException>(() => new Person(firstNameObj, null, contactInfo));
        }

        [Theory]
        [InlineData("Jhon", "Doe")]
        [InlineData("Jane", "Doe")]
        public void ConstructorWithInvalidContactInfoTest(string first, string lastName)
        {
            var firstNameObj = new Name(first);
            var lastNameObj = new Name(lastName);

            Assert.Throws<ArgumentNullException>(() => new Person(firstNameObj, lastNameObj, null));
        }

        [Fact]
        public void ConstructorWithAllInvalidParameters()
        {
            Assert.Throws<ArgumentNullException>(() => new Person(null, null, null));
        }
    }
}