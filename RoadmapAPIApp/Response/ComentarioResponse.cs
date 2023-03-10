namespace RoadmapAPIApp.Response;

public class ComentarioResponse
{
	public Guid Id { get; set; }
	public string Descricao { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime UpdatedDate { get; set; }
	
	public Guid UserId { get; set; }
	public Guid RoadmapId { get; set; }

}
