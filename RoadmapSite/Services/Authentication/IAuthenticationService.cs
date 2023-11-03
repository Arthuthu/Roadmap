using RoadmapBlazor.Models;

namespace RoadmapBlazor.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
    Task Logout();
}