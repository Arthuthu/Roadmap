using Site.Models;

namespace Site.Services.Authentication;

public interface IAuthenticationService
{
	Task<AuthenticatedUserModel?> Login(AuthenticationUserModel userForAuthentication);
	Task Logout();
}