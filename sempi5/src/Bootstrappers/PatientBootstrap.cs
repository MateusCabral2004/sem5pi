using Sempi5.Domain.PatientAggregate;
using Sempi5.Domain.PersonalData;
using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.PatientAggregate;

namespace Sempi5.Bootstrappers;

public class PatientBootstrap
{
    private readonly IPatientRepository _patientRepository;

    public PatientBootstrap(IPatientRepository patientRepo)
    {
        _patientRepository = patientRepo;
    }

    public async Task SeedActivePatients()
    {
        var user1 = new SystemUser(new Email("mateuscabral123321@gmail.com"), "Patient", true);
        var user2 = new SystemUser(new Email("mateuscabral20042@gmail.com"), "Patient", true);

        var patient1 = new Patient
        (
            user1, new Person(new Name("Alice"), new Name("Doe"),
                new ContactInfo(new Email("mateuscabral20042@gmail.com"), new PhoneNumber(987654322))),
            new DateTime(1990, 1, 10),
            "Combat Helicopter",
            new List<string> { "Peanuts", "Asthma" },
            "456",
            new List<string> { "01/01/2021 9am-10am", "02/02/2021 10am-11am" },
            PatientStatusEnum.ACTIVATED
        );

        var patient2 = new Patient
        (user2,
            new Person(new Name("Bob"), new Name("Smith"),
                new ContactInfo(new Email("mateuscabral123321@gmail.com"), new PhoneNumber(987654329))),
            new DateTime(1990, 1, 10),
            "Ambulance",
            new List<string> { "Shellfish", "Diabetes" },
            "789",
            new List<string> { "03/03/2021 9am-10am", "04/04/2021 10am-11am" },
            PatientStatusEnum.DEACTIVATED
        );
        
        await _patientRepository.AddAsync(patient1);
        await _patientRepository.AddAsync(patient2);
    }
}