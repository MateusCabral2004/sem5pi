using Sempi5.Infrastructure.AccoutToDeleteAggregate;
using Sempi5.Infrastructure.PatientAggregate;
using Sempi5.Infrastructure.PersonAggregate;
using Sempi5.Infrastructure.UserAggregate;

namespace Sempi5.Services;

public class CheckUserToDeleteService
{
    private readonly IAccountToDeleteRepository _accountToDeleteRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IPersonRepository _personRepository;

    public CheckUserToDeleteService(IAccountToDeleteRepository accountToDeleteRepository, IUserRepository userRepository, IPatientRepository patientRepository, IPersonRepository personRepository)
    {
        _accountToDeleteRepository = accountToDeleteRepository;
        _userRepository = userRepository;
        _patientRepository = patientRepository;
        _personRepository = personRepository;
    }



    public async Task checkUserToDelete()
    {
        try
        {
            var usersToDelete = await _accountToDeleteRepository.checkUserToDelete();
            foreach (var user in usersToDelete)
            {
                try
                {
                    var patient = await _patientRepository.getByUserId(user.AsLong());

                    if (patient.User != null)
                    {
                        await _userRepository.RemoveAsync(patient.User);
                    }
                    if (patient.Person != null)
                    {
                        await _personRepository.RemoveAsync(patient.Person);
                    }
                    patient.EmergencyContact=null;
                    await _accountToDeleteRepository.removeUserbyId(user);
                    await _patientRepository.SavePatientAsync(patient);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing user deletion for user ID: {user.AsLong()}", ex);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching users to delete.", ex);
        }
    }

}