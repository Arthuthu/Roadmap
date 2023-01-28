using RoadmapSite.Models;

namespace RoadmapSite.Authentication
{
	public interface IAuthenticationService
	{
		Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
		Task Logout();
	}
}