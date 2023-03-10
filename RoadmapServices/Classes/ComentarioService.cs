using RoadmapRepository.Interfaces;
using RoadmapRepository.Models;
using RoadmapServices.Interfaces;

namespace RoadmapServices.Classes;

public class ComentarioService : IComentarioService
{
	private readonly IComentarioRepository _comentarioRepository;

	public ComentarioService(IComentarioRepository comentarioRepository)
	{
		_comentarioRepository = comentarioRepository;
	}

	public Task<IEnumerable<ComentarioModel>> GetAllComentarios()
	{
		return _comentarioRepository.GetAllComentarios();
	}

	public async Task<ComentarioModel?> GetComentarioById(Guid id)
	{
		return await _comentarioRepository.GetComentarioById(id);
	}

	public async Task CreateComentario(ComentarioModel comentario)
	{
		comentario.Id = Guid.NewGuid();
		comentario.CreatedDate = DateTime.UtcNow.AddHours(-3);
		await _comentarioRepository.CreateComentario(comentario);
	}

	public Task UpdateComentario(ComentarioModel comentario)
	{
		comentario.UpdatedDate = DateTime.UtcNow.AddHours(-3);
		return _comentarioRepository.UpdateComentario(comentario);
	}

	public Task DeleteComentario(Guid id)
	{
		return _comentarioRepository.DeleteComentario(id);
	}
}
