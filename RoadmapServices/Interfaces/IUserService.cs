using RoadmapRepository.Models;

namespace RoadmapServices.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel?> GetUserById(Guid id);
        Task<UserModel?> GetUserByName(UserModel user);
		Task<IList<string>> AddUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(Guid id);

        string Login(UserModel user);
        Task<string> ConcatRegistrationMessages(IList<string> responseMessages);
	}
}