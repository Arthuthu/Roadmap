﻿namespace RoadmapRepository.Models;

public class NodeModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }


	public Guid RoadmapId { get; set; }
}
