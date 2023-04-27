﻿namespace RoadmapAPIApp.Request;

public class UserRequest
{
	public Guid? Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public Guid? ConfirmationCode { get; set; }
	public DateTime? ConfirmationCodeExpirationDate { get; set; }
	public string? Bio { get; set; }
	public string? IsBanned { get; set; }
	public string? IsConfirmed { get; set; }
}
