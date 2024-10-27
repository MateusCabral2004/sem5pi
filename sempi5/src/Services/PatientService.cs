using Sempi5.Domain.ConfirmationLinkAggregate;
using Sempi5.Domain.ConfirmationTokenAggregate;
using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.AccoutToDeleteAggregate;
using Sempi5.Infrastructure.ConfirmationLinkAggregate;
using Sempi5.Infrastructure.ConfirmationTokenAggregate;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.LinkConfirmationRepository;
using Sempi5.Infrastructure.PatientAggregate;
using Sempi5.Infrastructure.PatientRepository;
using Sempi5.Infrastructure.PersonAggregate;
using Sempi5.Infrastructure.PersonRepository;

namespace Sempi5.Services;

public class PatientService
{
    private readonly DBContext context;
    private readonly IPatientRepository _patientRepository;
    private readonly IConfirmationTokenRepository _confirmationRepository;
    private readonly IConfirmationLinkRepository _confirmationLinkRepository;
    private readonly IAccountToDeleteRepository _accountToDeleteRepository;
    private readonly EmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;


    public PatientService(IPatientRepository patientRepository, EmailService emailService,
        IConfirmationTokenRepository confirmationRepository, IAccountToDeleteRepository accountToDeleteRepository,
        IConfirmationLinkRepository confirmationLinkRepository, IUnitOfWork unitOfWork,
        IPersonRepository personRepository)
    {
        _confirmationRepository = confirmationRepository;
        _patientRepository = patientRepository;
        _emailService = emailService;
        _accountToDeleteRepository = accountToDeleteRepository;
        _confirmationLinkRepository = confirmationLinkRepository;
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }

    private async Task<ConfirmationToken> RegisterToken(ConfirmationToken confirmationToken)
    {
        var token = await _confirmationRepository.GetByEmail(confirmationToken.email.ToString());

        if (token == null)
        {
            return await _confirmationRepository.AddAsync(confirmationToken);
        }

        if (!token.Id.Equals(confirmationToken.Id))
        {
            throw new ArgumentException(
                "Account already waiting for verification. If you made a mistake, please contact support.");
        }

        token.ResetExpiryDate();

        return token;
    }

    private async Task<ConfirmationLink> registerLink(ConfirmationLink confirmationLink)
    {
        var token = await _confirmationLinkRepository.GetByEmail(confirmationLink.email.ToString());
        if (token == null)
        {
            return await _confirmationLinkRepository.addAsync(confirmationLink);
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

        var userEmail = new Email(email);

        patient.User = new SystemUser(userEmail, "Patient");

        //TODO - Generate a token and send it to the user's email
        var confirmationToken = new ConfirmationToken(userEmail, "Confirm your account");
        
        try
        {
            var token = await RegisterToken(confirmationToken);
            await _patientRepository.SavePatientAsync(patient);
            await _emailService.SendPatientConfirmationEmail(email, token.Id.ToString());
        }
        catch (Exception e)
        {
            throw new Exception(e.Message + " And try checking your email for the confirmation link.");
        }

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

        // Atualizar somente se o valor não for nulo ou em branco
        if (!string.IsNullOrWhiteSpace(profileDto.firstName))
        {
            patient.Person.FirstName = new Name(profileDto.firstName);
        }

        if (!string.IsNullOrWhiteSpace(profileDto.lastName))
        {
            patient.Person.LastName = new Name(profileDto.lastName);
        }

        patient.Person.FullName =
            new Name($"{patient.Person.FirstName.ToString()} {patient.Person.LastName.ToString()}");

        if (!string.IsNullOrWhiteSpace(profileDto.email))
        {
            patient.Person.ContactInfo._email = new Email(profileDto.email);
        }

        if (!string.IsNullOrWhiteSpace(profileDto.phoneNumber))
        {
            if (int.TryParse(profileDto.phoneNumber, out int phoneNumber))
            {
                patient.Person.ContactInfo._phoneNumber = new PhoneNumber(phoneNumber);
            }
            else
            {
                throw new Exception("Número de telefone inválido.");
            }
        }

        if (!string.IsNullOrWhiteSpace(profileDto.birthDate))
        {
            // Tenta converter a string birthDate para DateTime
            if (DateTime.TryParse(profileDto.birthDate, out DateTime birthDate))
            {
                patient.BirthDate = birthDate;
            }
            else
            {
                throw new Exception("Formato de data inválido.");
            }
        }

        if (!string.IsNullOrWhiteSpace(profileDto.gender))
        {
            patient.Gender = profileDto.gender;
        }

        if (profileDto.allergiesAndMedicalConditions != null)
        {
            patient.AllergiesAndMedicalConditions = profileDto.allergiesAndMedicalConditions;
        }

        if (!string.IsNullOrWhiteSpace(profileDto.emergencyContact))
        {
            patient.EmergencyContact = profileDto.emergencyContact;
        }

        if (profileDto.appointmentHistory != null)
        {
            patient.AppointmentHistory = profileDto.appointmentHistory;
        }

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


    public async Task defineDataToExcludeAccount(string email)
    {
        Console.WriteLine("Iniciando exclusão de conta");
        var confirmationLink = new ConfirmationLink(new Email(email));
        Console.WriteLine("Email to delete: " + confirmationLink.email + " " + confirmationLink.Id + " " +
                          confirmationLink.ExpiryDate);
        var token = await registerLink(confirmationLink);
        await _confirmationLinkRepository.saveChangesAsync();
        await _emailService.SendPatientDeleteConfirmationEmail(email, token.Id.ToString());
    }

    public async Task excludeAccountSchedule(string token)
    {
        // Obtém o token de confirmação e verifica se ele é válido
        var confirmationToken = await _confirmationLinkRepository.GetByIdAndNotUsed(token);

        if (confirmationToken == null)
        {
            throw new InvalidOperationException("Token não encontrado ou já foi utilizado.");
        }

        Console.WriteLine("Token para exclusão: " + token);
        Console.WriteLine("Token de exclusão no objeto: " + confirmationToken.Id);
        Console.WriteLine("Email para exclusão: " + confirmationToken.email);

        var patient = await _patientRepository.GetByEmail(confirmationToken.email?.ToString());

        if (patient == null)
        {
            throw new InvalidOperationException("Paciente não encontrado com o email fornecido.");
        }

        var userID = patient.User?.Id.AsLong() ?? -1;

        if (userID == -1)
        {
            throw new InvalidOperationException("ID de usuário inválido.");
        }

        await _accountToDeleteRepository.saveUserToDelete(userID);
        Console.WriteLine("Usuário com ID " + userID + " adicionado à lista de exclusão.");
    }


    public async Task<List<SearchedPatientDTO>> ListPatientByName(NameDTO nameDto)
    {
        var patient = await _patientRepository.GetActivePatientsByName(new Name(nameDto.name));

        if (patient.Count == 0)
        {
            throw new ArgumentException("Patient not found");
        }

        return buildSearchedPatientDtoList(patient);
    }

    private SearchedPatientDTO patientToSearchedPatientDto(Patient patient)
    {
        return new SearchedPatientDTO
        {
            Id = patient.Id.AsString(),
            FullName = patient.Person.FullName.ToString(),
            Email = patient.Person.ContactInfo.email().ToString(),
            BirthDate = patient.BirthDate.ToString("MM/dd/yyyy")
        };
    }


    public async Task<SearchedPatientDTO> ListPatientByEmail(EmailDTO emailDto)
    {
        var patient = await _patientRepository.GetActivePatientByEmail(new Email(emailDto.email));

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        return patientToSearchedPatientDto(patient);
    }

    public async Task<List<SearchedPatientDTO>> ListPatientByDateOfBirth(DateDTO dateDto)
    {
        var patients =
            await _patientRepository.GetActivePatientsByDateOfBirth(new DateTime(dateDto.year, dateDto.month,
                dateDto.day));

        if (patients.Count == 0)
        {
            throw new ArgumentException("No patients found.");
        }

        return buildSearchedPatientDtoList(patients);
    }

    public async Task<SearchedPatientDTO> ListPatientByMedicalRecordNumber(PatientIdDto patientId)
    {
        var patient =
            await _patientRepository.GetActivePatientByMedicalRecordNumber(new MedicalRecordNumber(patientId.Id));

        if (patient == null)
        {
            throw new ArgumentException("Patient not found.");
        }

        return patientToSearchedPatientDto(patient);
    }

    private List<SearchedPatientDTO> buildSearchedPatientDtoList(IEnumerable<Patient> patients)
    {
        List<SearchedPatientDTO> searchedPatientDtoList = new List<SearchedPatientDTO>();

        foreach (var patient in patients)
        {
            searchedPatientDtoList.Add(patientToSearchedPatientDto(patient));
        }

        return searchedPatientDtoList;
    }

    public async Task DeletePatientProfile(string email)
    {
        if (email == null)
        {
            throw new ArgumentException("The email address can´t be null");
        }

        var patient = await _patientRepository.GetActivePatientByEmail(new Email(email));

        patient.Person = null;
        patient.EmergencyContact = null;
        patient.AllergiesAndMedicalConditions = null;

        await _unitOfWork.CommitAsync();
    }

    public async Task EditPatientProfile(PatientDTO editPatientDto)
    {
        if (editPatientDto.email == null)
        {
            throw new ArgumentException("The email address can´t be null");
        }

        var patient = await _patientRepository.GetByEmail(editPatientDto.email);

        if (patient == null)
        {
            throw new ArgumentException("Patient not found");
        }

        var originalEmail = patient.Person.ContactInfo._email;

        var originalPhoneNumber = patient.Person.ContactInfo._phoneNumber;


        if (editPatientDto.firstName != null)
        {
            patient.Person.FirstName = new Name(editPatientDto.firstName);
        }

        if (editPatientDto.lastName != null)
        {
            patient.Person.LastName = new Name(editPatientDto.lastName);
        }

        if (editPatientDto.birthDate != null)
        {
            patient.BirthDate = DateTime.Parse(editPatientDto.birthDate);
        }

        if (editPatientDto.phoneNumber == -1)
        {
            patient.Person.ContactInfo._phoneNumber = new PhoneNumber(editPatientDto.phoneNumber ?? 0);
        }

        if (editPatientDto.email != null)
        {
            patient.Person.ContactInfo._email = new Email(editPatientDto.email);
        }

        await _patientRepository.AddAsync(patient);
        await _unitOfWork.CommitAsync();

        if (originalEmail.ToString() != editPatientDto.email)
        {
            await _emailService.SendPatientUpdatingEmail_EmailAltered(originalEmail.ToString(), editPatientDto.email);
        }

        if (originalPhoneNumber.phoneNumber() != editPatientDto.phoneNumber)
        {
            await _emailService.SendPatientUpdatingEmail_PhoneNumberAltered(originalEmail.ToString(),
                editPatientDto.phoneNumber.ToString());
        }
    }

    private Patient patientDTOToPatient(PatientDTO patientDTO)
    {
        return new Patient(new Person(new Name(patientDTO.firstName), new Name(patientDTO.lastName),
                new ContactInfo(new Email(patientDTO.email), new PhoneNumber(patientDTO.phoneNumber ?? 0))),
            DateTime.Parse(patientDTO.birthDate), patientDTO.gender, patientDTO.emergencyContact);
    }

    public async Task VerifyPhoneNumberAvailability(PhoneNumber phoneNumber)
    {
        var personByPhoneNumber = await _personRepository.GetPersonByPhoneNumber(phoneNumber);

        if (personByPhoneNumber != null)
        {
            throw new ArgumentException("Phone Number already in use");
        }
    }

    public async Task VerifyEmailAvailability(Email email)
    {
        var personByEmail = await _personRepository.GetPersonByEmail(email);
        if (personByEmail != null)
        {
            throw new ArgumentException("Email already in use");
        }
    }

    public async Task CreatePatientProfile(PatientDTO patientDTO)
    {
        var phoneNumber = new PhoneNumber(patientDTO.phoneNumber ?? 0);
        await VerifyPhoneNumberAvailability(phoneNumber);

        var email = new Email(patientDTO.email);
        await VerifyEmailAvailability(email);
        
        var patient = patientDTOToPatient(patientDTO);
        await _patientRepository.AddAsync(patient);

        await _unitOfWork.CommitAsync();
    }
}