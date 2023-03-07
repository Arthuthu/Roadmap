using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapSite.Models;
using RoadmapSite.Services.RoadmapVotes.Interfaces;
using RoadmapSite.Services.User.Interfaces;
using RoadmapSite.Services.Voting.Interfaces;
using System.Security.Claims;

namespace RoadmapSite.Services.Voting.Classes;

public class VotingService : IVotingService
{
	private readonly NavigationManager _navigationManager;
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	private readonly IRoadmapVotesService _roadmapVotesService;

	public VotingService(NavigationManager navigationManager,
		AuthenticationStateProvider authenticationStateProvider,
		IRoadmapVotesService roadmapVotesService)
	{
		_navigationManager = navigationManager;
		_authenticationStateProvider = authenticationStateProvider;
		_roadmapVotesService = roadmapVotesService;
	}

	public async Task<Guid> GetLoggedInUserId()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		var user = authenticationState.User;
		var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (userId is null)
		{
			return Guid.Empty;
		}

		return new Guid(userId);
	}

	public async Task AddUserVote(Guid roadmapId, Guid? loggedInUserId)
	{
		if (loggedInUserId == Guid.Empty)
		{
			_navigationManager.NavigateTo("/login");
		}

		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();

		var votedRoadmapId = roadmapVotes!
			.Where(x => x.UserId == loggedInUserId && x.RoadmapId == roadmapId)
			.Select(x => x.Id).FirstOrDefault();

		if (votedRoadmapId != Guid.Empty)
		{
			await _roadmapVotesService.RemoveRoadmapVote(votedRoadmapId);
		}
		else
		{
			await _roadmapVotesService.AddRoadmapVote(loggedInUserId, roadmapId);
		}
	}

	public async Task<string> GetButtonColor(Guid roadmapId, Guid? loggedInUserId)
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();

		var votedRoadmapId = roadmapVotes!
			.Where(x => x.UserId == loggedInUserId && x.RoadmapId == roadmapId)
			.Select(x => x.Id).FirstOrDefault();

		if (votedRoadmapId == Guid.Empty)
		{
			return "vote-button";

		}
		else
		{
			return "vote-button-voted";
		}
	}

	public async Task<int> GetRoadmapVotes(Guid roadmapId)
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes();

		if (roadmapVotes is null)
		{
			return 0;
		}

		var roadmapVoteCount = roadmapVotes.Where(x => x.RoadmapId == roadmapId).Count();

		return roadmapVoteCount;
	}
}
