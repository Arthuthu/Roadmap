using RoadmapRepository.Models;

namespace RoadmapRepository.User
{
	public interface IUserRepository
	{
		Task<IEnumerable<UserModel>> GetAllUsers();
		Task<UserModel?> GetUserById(Guid id);
		Task AddUser(UserModel user);
		Task UpdateUser(UserModel user);
		Task DeleteUser(Guid id);
	}
}