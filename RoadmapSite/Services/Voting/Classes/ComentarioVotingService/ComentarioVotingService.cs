﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapSite.Services.ComentarioVotes.Interfaces;
using RoadmapSite.Services.Voting.Interfaces.ComentarioVotingService;

namespace ComentarioSite.Services.Voting.Classes.ComentarioVotingService;

public class ComentarioVotingService : IComentarioVotingService
{
	private readonly NavigationManager _navigationManager;
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	private readonly IComentarioVotesService _comentarioVotesService;

	public ComentarioVotingService(NavigationManager navigationManager,
		AuthenticationStateProvider authenticationStateProvider,
		IComentarioVotesService comentarioVotesService)
	{
		_navigationManager = navigationManager;
		_authenticationStateProvider = authenticationStateProvider;
		_comentarioVotesService = comentarioVotesService;
	}

	public async Task AddUserVote(Guid comentarioId, Guid? loggedInUserId)
	{
		if (loggedInUserId == Guid.Empty)
		{
			_navigationManager.NavigateTo("/login");
		}

		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes();

		var votedComentarioId = comentarioVotes!
			.Where(x => x.UserId == loggedInUserId && x.ComentarioId == comentarioId)
			.Select(x => x.Id).FirstOrDefault();

		if (votedComentarioId != Guid.Empty)
		{
			await _comentarioVotesService.RemoveComentarioVote(votedComentarioId);
		}
		else
		{
			await _comentarioVotesService.AddComentarioVote(loggedInUserId, comentarioId);
		}
	}

	public async Task<string> GetButtonColor(Guid comentarioId, Guid? loggedInUserId)
	{
		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes();

		var votedComentarioId = comentarioVotes!
			.Where(x => x.UserId == loggedInUserId && x.ComentarioId == comentarioId)
			.Select(x => x.Id).FirstOrDefault();

		if (votedComentarioId == Guid.Empty)
		{
			return "comentario-vote-button";

		}
		else
		{
			return "comentario-vote-button-voted";
		}
	}

	public async Task<int> GetComentarioVotes(Guid comentarioId)
	{
		var comentarioVotes = await _comentarioVotesService.GetAllComentarioVotes();

		if (comentarioVotes is null)
		{
			return 0;
		}

		var comentarioVoteCount = comentarioVotes.Where(x => x.ComentarioId == comentarioId).Count();

		return comentarioVoteCount;
	}
}