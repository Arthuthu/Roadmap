﻿namespace RoadmapSite.Models;

public class RoadmapClassModel
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string Category { get; set; }
	public string Author { get; set; }

	public Guid UserId { get; set; }
}