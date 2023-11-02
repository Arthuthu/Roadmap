using Domain.Interfaces;
using Domain.Models;
using Infra.Interfaces;

namespace Infra.Services;

public class ComentarioService : IComentarioService
{
	private readonly IComentarioRepository _comentarioRepository;

	public ComentarioService(IComentarioRepository comentarioRepository)
	{
		_comentarioRepository = comentarioRepository;
	}

	public Task<IEnumerable<ComentarioModel>> GetAllComentarios(Guid roadmapId)
	{
		return _comentarioRepository.GetAllComentarios(roadmapId);
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
	public Task DeleteAllUserComentarios(Guid userId)
	{
		return _comentarioRepository.DeleteAllUserComentarios(userId);
	}

	public Task DeleteComentario(Guid id)
	{
		return _comentarioRepository.DeleteComentario(id);
	}
}
