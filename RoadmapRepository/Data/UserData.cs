using RoadmapRepository.Models;
using RoadmapRepository.SqlDataAccess;

namespace RoadmapRepository.Data;

public class UserData : IUserData
{
	private readonly ISqlDataAccess _db;

	public UserData(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<UserModel>> GetUsers()
	{
		return _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });
	}

	public async Task<UserModel?> GetUser(Guid id)
	{
		var results = await _db.LoadData<UserModel, dynamic>(
			"dbo.spUser_GetById",
			new { Id = id });

		return results.FirstOrDefault();
	}

	public Task InsertUser(UserModel user)
	{
		return _db.SaveData("dbo.spUser_Add", new { user.Username, user.Password });
	}

	public Task UpdateUser(UserModel user)
	{
		return _db.SaveData("dbo.spUser_Update", user);
	}

	public Task DeleteUser(Guid id)
	{
		return _db.SaveData("dbo.spUser_Delete", new { Id = id });
	}
}
