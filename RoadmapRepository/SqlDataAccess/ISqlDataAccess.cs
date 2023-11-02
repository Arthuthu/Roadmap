namespace Domain.SqlDataAccess
{
	public interface ISqlDataAccess
	{
		Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "RoadmapConnection");
		Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "RoadmapConnection");
	}
}