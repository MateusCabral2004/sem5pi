using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sempi5.Domain.PatientAggregate;

namespace Sempi5.Tests.src.Domain.Patient
{
    [TestClass]
    public class PatientTest
    {
      /*  [TestMethod]
        public void CreatePatient_ShouldCreateCorrectly()
        {
            // Arrange
            var patient = new Sempi5.Domain.Patient.Patient("test_user", "email@test.com", "123456789");

            // Act
           // var profile = patient.CreateProfile();

            // Assert
            Assert.AreEqual("test_user", patient.FirstName);,
            Assert.AreEqual("email@test.com", patient.ContactInfo);
            Assert.AreEqual("123456789", patient.MedicalRecordNumber);
        }

        [TestMethod]
        public void SendEmailValidation_ShouldSendAndValidateEmail()
        {
            // Arrange
            var patient = new Sempi5.Domain.Patient.Patient("test_user", "email@test.com", "123456789");

            // Act
            var emailSent = patient.SendEmailConfirmation();

            // Assert - Check if the email was sent
            Assert.IsTrue(emailSent);
            Assert.IsFalse(patient.IsConfirmed);

            // Simulate email confirmation
            patient.ConfirmEmail();

            // Assert - Check if the email was validated
            Assert.IsTrue(patient.IsConfirmed);
        }

        [TestMethod]
        public void PatientCannotListAppointmentsWithoutEmailValidation()
        {
            // Arrange
            var patient = new Sempi5.Domain.Patient.Patient("test_user", "email@test.com", "123456789", false);

            // Act
            var appointments = patient.ListAppointments();

            // Assert - Should return null or empty because email is not validated
            Assert.IsNull(appointments);
        }

        [TestMethod]
        public void PatientCanListAppointmentsAfterEmailValidation()
        {
            // Arrange
            var patient = new Sempi5.Domain.Patient.Patient("test_user", "email@test.com", "123456789", true);

            // Act
            var appointments = patient.ListAppointments();

            // Assert - Should return the list of appointments
            Assert.IsNotNull(appointments);
        }
       */
    }
}