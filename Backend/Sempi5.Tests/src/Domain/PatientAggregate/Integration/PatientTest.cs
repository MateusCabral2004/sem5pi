using System;
using System.Collections.Generic;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Xunit;

namespace Sempi5.Tests.Domain.OperationTypeAggregate.Integration
{
    public class PatientTest
    {
        [Fact]
        public void TestPatientConstructorWithValidParameters()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            var obj = new Patient(
                user,
                person,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            );

            Assert.NotNull(obj);
        }

        [Fact]
        public void TestConstructorWithNullUser()
        {
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                null,
                person,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }

        [Fact]
        public void TestConstructorWithNullPerson()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user,
                null,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }

        [Fact]
        public void TestPatientConstructorWithNoGender()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentException>(() => new Patient(
                user,
                person,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }

        [Fact]
        public void TestPatientConstructorWithNoEmergencyContact()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentException>(() => new Patient(
                user,
                person,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }

        [Fact]
        public void TestPatientConstructorWithNoAllergies()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "M";
            var emergencyContact = "4295";
            var appointmentHistory = new List<string> { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user,
                person,
                dateTime,
                gender,
                null,
                emergencyContact,
                appointmentHistory
            ));
        }

        [Fact]
        public void TestPatientConstructorWithNoHistory()
        {
            var email = new Email("mat@gmail.com");
            var user = new SystemUser(email, "testRole");
            var firstName = new Name("John");
            var lastName = new Name("Doe");
            var phoneNumber = new PhoneNumber(987654321);
            var contactInfo = new ContactInfo(new Email("rui@gmail.com"), phoneNumber);
            var person = new Person(firstName, lastName, contactInfo);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string> { "Allergy1", "Allergy2" };
            var emergencyContact = "324324";

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user,
                person,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                null
            ));
        }
    }
}