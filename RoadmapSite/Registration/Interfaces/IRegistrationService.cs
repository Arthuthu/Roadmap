using RoadmapSite.Models;

namespace RoadmapSite.Registration.Interfaces
{
    public interface IRegistrationService
    {
        Task<string> RegisterUser(RegistrationModel registrationUser);
    }
}