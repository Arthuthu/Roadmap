using Site.Models;

namespace Site.Services.Registration;

public interface IRegistrationService
{
	Task<string?> RegisterUser(RegistrationModel registrationUser);
}