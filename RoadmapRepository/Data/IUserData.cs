using RoadmapRepository.Models;

namespace RoadmapRepository.Data
{
	public interface IUserData
	{
		Task DeleteUser(Guid id);
		Task<UserModel?> GetUserById(Guid id);
		Task<IEnumerable<UserModel>> GetAllUsers();
		Task AddUser(UserModel user);
		Task UpdateUser(UserModel user);
	}
}