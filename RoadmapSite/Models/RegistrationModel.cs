﻿using System.ComponentModel.DataAnnotations;

namespace RoadmapSite.Models;

public class RegistrationModel
{
	public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; 
    public string ConfirmPassword { get; set; } = string.Empty;

}
