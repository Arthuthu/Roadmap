using RoadmapSite.Models;

namespace RoadmapSite.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
    Task Logout();
}