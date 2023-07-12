using RoadmapSite.Models;

namespace RoadmapSite.Services.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}