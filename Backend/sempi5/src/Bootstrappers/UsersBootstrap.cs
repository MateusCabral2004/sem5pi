using Sempi5.Domain.Shared;
using Sempi5.Domain.User;
using Sempi5.Infrastructure.UserAggregate;

namespace Sempi5.Bootstrappers;

public class UsersBootstrap
{
    private readonly IUserRepository _userRepository;
    
    public UsersBootstrap(IUserRepository userRepo)
    {
        _userRepository = userRepo;
    }

    public async Task SeedAdminUser()
    {
        var adminEmail = new Email("admsem5projintegra@gmail.com");
        var admin = new SystemUser(adminEmail, "Admin", true);

        await _userRepository.AddAsync(admin);
    }

}