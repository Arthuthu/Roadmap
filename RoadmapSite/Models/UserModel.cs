namespace RoadmapSite.Models;

public class UserModel
{
	public Guid Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string? Password { get; set; }
	public Guid? ConfirmationCode { get; set; }
	public DateTime? ConfirmationCodeExpirationDate { get; set; }
	public string? Bio { get; set; }
	public bool IsAdmin { get; set; }
	public bool IsBanned { get; set; }
	public bool IsConfirmed { get; set; }
}
