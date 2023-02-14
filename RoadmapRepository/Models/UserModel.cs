namespace RoadmapRepository.Models;

public class UserModel
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
	public DateTime CreatedDate { get; set; }
}
