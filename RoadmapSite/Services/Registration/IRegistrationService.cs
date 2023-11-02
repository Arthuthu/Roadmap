using RoadmapSite.Models;

namespace RoadmapSite.Services.Registration;

public interface IRegistrationService
{
    Task<string?> RegisterUser(RegistrationModel registrationUser);
}