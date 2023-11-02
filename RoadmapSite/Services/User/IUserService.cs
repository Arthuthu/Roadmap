using Site.Models;

namespace Site.Services.User;

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