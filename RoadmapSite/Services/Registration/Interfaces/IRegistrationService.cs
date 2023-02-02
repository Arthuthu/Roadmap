using RoadmapSite.Models;

namespace RoadmapSite.Services.Registration.Interfaces
{
    public interface IRegistrationService
    {
        Task<string> RegisterUser(RegistrationModel registrationUser);
    }
}