using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sempi5.Domain.ConfirmationToken;
using Sempi5.Domain.Patient;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.ConfirmationTokenRepository;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.PatientRepository;

namespace Sempi5.Services;

public class PatientService
{
    private readonly DBContext context;
    private readonly IPatientRepository _patientRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly EmailService _emailService;
    public PatientService(IPatientRepository patientRepository,EmailService emailService, IConfirmationTokenRepository confirmationRepository)
    {
        _confirmationRepository = confirmationRepository;
        _patientRepository = patientRepository;
        _emailService = emailService;
    }
    
    private async Task<ConfirmationToken> RegisterToken(ConfirmationToken confirmationToken)
    {
        var token = await _confirmationRepository.GetByEmail(confirmationToken.email.ToString());

        if (token == null)
        {
            return await _confirmationRepository.AddAsync(confirmationToken);
        }
        
        if(!token.Id.Equals(confirmationToken.Id))
        {
            throw new ArgumentException("Account already waiting for verification with a different patient profile. If you made a mistake, please contact support.");
        }
        
        token.ResetExpiryDate();
        
        return token;

    }

    public async Task<bool> RegisterPatientUser(string email, int number)
    {
        
        var patient = await _patientRepository.GetByPhoneNumber(number);

        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }

        if (patient.User.IsVerified)
        {
            throw new Exception("Email já confirmado");
        }
        var userEmail= new Email(email);

        patient.User = new SystemUser(userEmail, "Patient");
        
        var confirmationToken = new ConfirmationToken(userEmail, "patient.Id.AsString()");

        var token = await RegisterToken(confirmationToken);

        _emailService.SendPatientConfirmationEmail(email, token.Id.ToString());
        
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

    public async Task updateAccount(PatientProfileDto profileDto, string email)
    {
        var patient = await _patientRepository.GetByEmail(email);
        if (patient == null)
        {
            throw new Exception("Paciente não encontrado");
        }
        var person = new Person(new Name(profileDto.firstName), new Name(profileDto.lastName),
            new ContactInfo(new Email(profileDto.email), new PhoneNumber(profileDto.phoneNumber)));
        
        patient.Person.FirstName = person.FirstName;
        patient.Person.LastName = person.LastName;
        patient.Person.ContactInfo = person.ContactInfo;
        patient.Person.FullName = person.FullName;
        patient.BirthDate = profileDto.birthDate;
        patient.Gender = profileDto.gender;
        patient.AllergiesAndMedicalConditions = profileDto.allergiesAndMedicalConditions;
        patient.EmergencyContact = profileDto.emergencyContact;
        patient.AppointmentHistory = profileDto.appointmentHistory;

        await _patientRepository.SavePatientAsync(patient);
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