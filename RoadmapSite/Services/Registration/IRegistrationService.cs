using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.Registration;

public interface IRegistrationService
{
    Task<string?> RegisterUser(RegistrationModel registrationUser);
}