namespace RoadmapSite.Models;

public class UserModel
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string? Password { get; set; }
	public string? Bio { get; set; }
	public string IsAdmin { get; set; }
	public string IsBanned { get; set; }
}
