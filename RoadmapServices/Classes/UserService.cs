using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapServices.Classes;

public class UserService : IUserService
{
    private readonly IUserRepository _userData;

    public UserService(IUserRepository userData)
    {
        _userData = userData;
    }

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _userData.GetAllUsers();
    }

    public async Task<UserModel?> GetUserById(Guid id)
    {
        return await _userData.GetUserById(id);
    }

    public async Task AddUser(UserModel user)
    {
        await _userData.AddUser(user);
    }

    public Task UpdateUser(UserModel user)
    {
        return _userData.UpdateUser(user);
    }

    public Task DeleteUser(Guid id)
    {
        return _userData.DeleteUser(id);
    }
}
