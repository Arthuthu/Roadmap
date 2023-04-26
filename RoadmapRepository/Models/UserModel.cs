namespace RoadmapRepository.Models;

public class UserModel
{
	public Guid? Id { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public Guid? ConfirmationCode { get; set; }
	public DateTime? ConfirmationCodeExpirationDate { get; set; }
	public string? Bio { get; set; }
	public int? IsAdmin { get; set; }
	public int? IsConfirmed { get; set; }
	public int? IsBanned { get; set; }

	public byte[]? PasswordHash { get; set; }
	public byte[]? PasswordSalt { get; set; }


	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }
}
