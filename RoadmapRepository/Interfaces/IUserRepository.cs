using RoadmapRepository.Models;

namespace RoadmapRepository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetUserById(Guid id);
        Task<UserModel?> GetUserByConfirmationCode(Guid confirmationCode);
		Task<UserModel?> GetUserByName(UserModel user);
        Task<UserModel?> GetUserByEmail(UserModel user);
		Task AddUser(UserModel user);
        Task UpdateUserEmailConfirmation(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);


    }
}