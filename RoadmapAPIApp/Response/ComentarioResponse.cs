﻿namespace RoadmapAPIApp.Response;

public class ComentarioResponse
{
	public Guid Id { get; set; }
	public string Comentario { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
	
	public Guid UserId { get; set; }
}
