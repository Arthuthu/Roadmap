using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Classes;

public class UserRepository : IUserRepository
{
    private readonly ISqlDataAccess _db;

    public UserRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });
    }

    public async Task<UserModel?> GetUserById(Guid id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "dbo.spUser_GetById",
            new { Id = id });

        return results.FirstOrDefault();
    }

	public async Task<UserModel?> GetUserByName(UserModel user)
	{
		var results = await _db.LoadData<UserModel, dynamic>(
			"dbo.spUser_GetByName",
			new { Username = user.Username });

		return results.FirstOrDefault();
	}

	public Task AddUser(UserModel user)
    {
        return _db.SaveData("dbo.spUser_Add",
        new { 
            user.Id,
            user.Username,
            user.Password,
            user.PasswordHash,
            user.PasswordSalt,
            user.CreatedDate
        });
    }

    public Task UpdateUser(UserModel user)
    {
        return _db.SaveData("dbo.spUser_Update", new 
        {
            user.Id,
            user.Username,
            user.Password,
            user.Bio
        });

    }

    public Task DeleteUser(Guid id)
    {
        return _db.SaveData("dbo.spUser_Delete", new { Id = id });
    }
}
