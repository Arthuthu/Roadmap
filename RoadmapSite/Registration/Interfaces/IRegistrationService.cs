using RoadmapSite.Models;

namespace RoadmapSite.Registration.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationModel> RegisterUser(RegistrationModel registrationUser);
    }
}