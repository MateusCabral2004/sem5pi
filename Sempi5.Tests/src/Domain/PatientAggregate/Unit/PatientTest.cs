using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Xunit;
using Assert = Xunit.Assert;

namespace Sempi5.Tests.src.Domain.PatientAggregate.Unit
{
    public class PatientTest
    {
        [Fact]
        public void testPatientConstructorWithValidParameters()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };
            var patientStatus = PatientStatusEnum.ACTIVATED;

            var obj = new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            );

            Assert.NotNull(obj);
        }

        [Fact]
        public void testPatientConstructorWithValidParameters2()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            var obj = new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            );

            Assert.NotNull(obj);
        }

        [Fact]
        public void testConstructorWithNullUser()
        {
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                null,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }
        
        [Fact]
        public void testConstructorWithNullPerson()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user.Object,
                null,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }
        
        [Fact]
        public void testPatientConstructorWithNoGender()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "EmergencyContact";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentException>(() =>  new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }
        
        [Fact]
        public void testPatientConstructorWithNoEmergencyContact()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentException>(() => new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                appointmentHistory
            ));
        }
        
        [Fact]
        public void testPatientConstructorWithNoAllergies()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var emergencyContact = "4295";
            var appointmentHistory = new List<string>() { "Appointment1", "Appointment2" };

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                null,
                emergencyContact,
                appointmentHistory
            ));
        }
        
        [Fact]
        public void testPatientConstructorWithNotHistory()
        {
            var email = new Mock<Email>("mat@gmail.com");
            var user = new Mock<SystemUser>(email.Object, "testRole");
            var firstName = new Mock<Name>("John");
            var lastName = new Mock<Name>("Doe");
            var phoneNumber = new Mock<PhoneNumber>(987654321);
            var emailContactInfo = new Mock<Email>("rui@gmail.com");
            var contactInfo = new Mock<ContactInfo>(emailContactInfo.Object, phoneNumber.Object);
            var person = new Mock<Person>(firstName.Object, lastName.Object, contactInfo.Object);
            var dateTime = DateTime.Now;
            var gender = "M";
            var allergiesAndMedicalConditions = new List<string>() { "Allergy1", "Allergy2" };
            var emergencyContact = "324324";

            Assert.Throws<ArgumentNullException>(() => new Patient(
                user.Object,
                person.Object,
                dateTime,
                gender,
                allergiesAndMedicalConditions,
                emergencyContact,
                null
            ));
        }
    }
}