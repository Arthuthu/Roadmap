namespace RoadmapAPI.Response;

public class UserResponse
{
	public Guid Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public Guid ConfirmationCode { get; set; }
	public DateTime? ConfirmationCodeExpirationDate { get; set; }
	public Guid? RestorationCode { get; set; }
	public DateTime? RestorationCodeExpirationDate { get; set; }
	public string Bio { get; set; } = string.Empty;
	public bool IsAdmin { get; set; }
	public bool IsBanned { get; set; }
	public bool IsConfirmed { get; set; }

	public DateTime? CreatedDate { get; set; }
	public DateTime? UpdatedDate { get; set; }

}
