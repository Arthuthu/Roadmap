using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapServices.Classes;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public async Task<UserModel?> GetUserById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task AddUser(UserModel user)
    {
        var users = _userRepository.GetAllUsers();

        foreach (var u in users.Result)
        {
            if (user.Username == u.Username)
            {
                throw new Exception("O nome de usuario ja esta cadastrado");
            }
        }

        await _userRepository.AddUser(user);
    }

    public Task UpdateUser(UserModel user)
    {
        return _userRepository.UpdateUser(user);
    }

    public Task DeleteUser(Guid id)
    {
        return _userRepository.DeleteUser(id);
    }
}
