namespace RoadmapServices.Exceptions;

internal class UsuarioNaoTemRoadmapsCriadoException : Exception
{
	public UsuarioNaoTemRoadmapsCriadoException() 
		: base("Usuario não tem roadmaps criados") 
	{
	}
}
