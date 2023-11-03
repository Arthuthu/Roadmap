namespace Roadmap.Infra.Exceptions;

internal class UsuarioNaoTemRoadmapsCriadoException : Exception
{
    public UsuarioNaoTemRoadmapsCriadoException()
        : base("Usuario não tem roadmaps criados")
    {
    }
}
