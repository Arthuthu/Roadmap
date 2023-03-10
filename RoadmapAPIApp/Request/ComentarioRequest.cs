namespace RoadmapAPIApp.Request;

public class ComentarioRequest
{
	public Guid Id { get; set; }
	public string Comentario { get; set; }

	public Guid UserId { get; set; }
}

