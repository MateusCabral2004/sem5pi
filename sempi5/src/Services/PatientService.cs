using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PatientRepository;

namespace Sempi5.Services;

public class PatientService
{
    private readonly DBContext context;
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<bool> RegisterPatientUser(string email, int number)
    {
        var patient = await _patientRepository.GetByPhoneNumber(number);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        patient.User = new SystemUser(new Email(email), "Patient");


        await _patientRepository.SavePatientAsync(patient);

        return true;
    }

    public async Task confirmEmail(string email, string token)
    {
        var patient = await _patientRepository.GetByEmail(email);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        if (patient.User.IsVerified)
        {
            throw new Exception("Email já confirmado");
        }


        patient.User.IsVerified = true;

        await _patientRepository.SavePatientAsync(patient);
    }

    public async Task updateAccount(PatientProfileDto profileDto)
    {
        var patient = await _patientRepository.GetByEmail(profileDto.email);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        if (patient.User.IsVerified)
        {
            if (profileDto.email != null)
            {
                //send Email to new email for confirmation
                patient.User.Email = new Email(profileDto.email);
            }

            var person = new Person(new Name(profileDto.firstName), new Name(profileDto.lastName),
                new ContactInfo(new Email(profileDto.email), new PhoneNumber(profileDto.phoneNumber)));
            patient.Person = person;
            patient.BirthDate = profileDto.birthDate;
            patient.Gender = profileDto.gender;
            patient.AllergiesAndMedicalConditions = profileDto.allergiesAndMedicalConditions;
            patient.EmergencyContact = profileDto.emergencyContact;
            patient.AppointmentHistory = profileDto.appointmentHistory;
        }
    }

    public async Task<List<string>> appointmentList(string email)
    {
        var patient = await _patientRepository.GetByEmail(email);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        if (patient.User.IsVerified)
        {
            return patient.AppointmentHistory;
        }

        //retornar null ou uma lista vazia? melhor seria retornar request denied unauthorized acess(you need to confirm your account)
        return null;
    }

    public async Task<bool> UserExist(string email)
    {
        var patient = await _patientRepository.GetByEmail(email);
        return patient.User != null;
    }


    public async Task excludeAccount(string email)
    {
        var patient = await _patientRepository.GetByEmail(email);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        if (patient.User.IsVerified)
        {
            // await patientRepository.DeletePatientAsync(patient);
        }
    }
}