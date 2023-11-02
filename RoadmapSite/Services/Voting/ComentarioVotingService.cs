using Microsoft.AspNetCore.Components;
using Site.Services.ComentarioVotes;

namespace Site.Services.Voting;

public class ComentarioVotingService : IComentarioVotingService
{
	private readonly NavigationManager _navigationManager;
	private readonly IComentarioVotesService _comentarioVotesService;

	public ComentarioVotingService(NavigationManager navigationManager,
		IComentarioVotesService comentarioVotesService)
	{
		_navigationManager = navigationManager;
		_comentarioVotesService = comentarioVotesService;
	}

	public async Task AddUserVote(Guid? loggedInUserId, Guid comentarioId)
	{
		if (loggedInUserId == Guid.Empty)
		{
			_navigationManager.NavigateTo("/login");
		}

		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes(loggedInUserId, comentarioId);

		var votedComentarioId = comentarioVotes!.Select(x => x.Id).FirstOrDefault();

		if (votedComentarioId != Guid.Empty)
		{
			await _comentarioVotesService.RemoveComentarioVote(votedComentarioId);
		}
		else
		{
			await _comentarioVotesService.AddComentarioVote(loggedInUserId, comentarioId);
		}
	}

	public async Task<string> GetButtonColor(Guid? loggedInUserId, Guid comentarioId)
	{
		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes(loggedInUserId, comentarioId);

		var votedComentarioId = comentarioVotes!.Select(x => x.Id).FirstOrDefault();

		if (votedComentarioId == Guid.Empty)
		{
			return "comment-vote-button";

		}
		else
		{
			return "comment-vote-button-voted";
		}
	}

	public async Task<int> GetComentarioVotes(Guid? loggedInUserId, Guid comentarioId)
	{
		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes(loggedInUserId, comentarioId);

		if (comentarioVotes is null)
		{
			return 0;
		}

		var comentarioVoteCount = comentarioVotes.Where(x => x.ComentarioId == comentarioId).Count();

		return comentarioVoteCount;
	}
}
