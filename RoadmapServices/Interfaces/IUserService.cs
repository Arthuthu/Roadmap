using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetUserById(Guid id);
        Task<UserModel?> GetUserByName(string username);
        Task<UserModel?> GetUserByConfirmationCode(Guid confirmationCode);
        Task<UserModel?> GetUserByRestorationCode(Guid restorationCode);
		Task<IList<string?>> AddUser(UserModel user);
		Task<IEnumerable<string?>> Login(UserModel user);
		Task UpdateUserEmailConfirmation(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);
        Task SendRestorationEmail(UserModel user);

	}
}