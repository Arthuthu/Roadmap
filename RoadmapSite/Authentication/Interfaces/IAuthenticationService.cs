using RoadmapSite.Models;

namespace RoadmapSite.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}