using RoadmapSite.Models;

namespace RoadmapSite.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(Guid? userId);
        Task<UserModel> GetUserByName(string? username);
        Task<string> UpdateUser(UserModel user);
        Task<string> UpdateUserEmailConfirmation(UserModel user);
		Task<Guid> GetLoggedInUserId();
        Task<UserModel> GetUserByConfirmationCode(Guid? confirmationCode);
        Task<string> SendRestorationEmail(UserModel user);
	}
}