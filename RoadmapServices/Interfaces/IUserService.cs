using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetUserById(Guid id);
        Task<UserModel?> GetUserByName(UserModel user);
        Task<UserModel?> GetUserByConfirmationCode(Guid confirmationCode);
		Task<IList<string>> AddUser(UserModel user);
        Task UpdateUserEmailConfirmation(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);

        Task<string> Login(UserModel user);
	}
}