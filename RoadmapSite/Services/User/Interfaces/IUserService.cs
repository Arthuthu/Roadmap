using RoadmapSite.Models;

namespace RoadmapSite.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserModel>?> GetAllUsers();
		Task<Guid> GetLoggedInUserId();
		Task<UserModel?> GetUserById(Guid? userId);
        Task<UserModel?> GetUserByName(string? username);
		Task<UserModel?> GetUserByConfirmationCode(Guid? confirmationCode);
		Task<UserModel?> GetUserByRestorationCode(Guid? restorationCode);
		Task<string?> UpdateUser(UserModel user);
        Task<string?> UpdateUserPassword(UserModel user);
		Task<string?> UpdateUserEmailConfirmation(UserModel user);
        Task<string?> SendConfirmationEmail(UserModel user);
		Task<string?> SendRestorationEmail(UserModel user);
	}
}