﻿namespace RoadmapAPIApp.Request;

public class UserRequest
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Username { get; set; }
	public string Password { get; set; }
}
