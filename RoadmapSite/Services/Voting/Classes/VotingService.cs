using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RoadmapSite.Models;
using RoadmapSite.Services.RoadmapVotes.Interfaces;
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

		RoadmapVotesModel roadmapVote = new();
		roadmapVote.UserId = loggedInUserId!;
		roadmapVote.RoadmapId = roadmapId;

		var didUserVotedOnRoadmap = await VerifiyIfUserVotedOnRoadmap(roadmapId, loggedInUserId);

		if (didUserVotedOnRoadmap is true)
		{
			var votedRoadmapId = await GetRoadmapVoteIdByUserAndRoadmapId(roadmapVote.RoadmapId, loggedInUserId);
			await _roadmapVotesService.RemoveRoadmapVote(votedRoadmapId);
		}
		else
		{
			await _roadmapVotesService.AddRoadmapVote(roadmapVote);
		}
	}

	private async Task<bool> VerifiyIfUserVotedOnRoadmap(Guid roadmapId, Guid? loggedInUserId)
	{
		if (loggedInUserId.ToString() is not null)
		{
			var roadmaps = await GetAllRoadmapsUserVoted(loggedInUserId);

			foreach (var roadmap in roadmaps)
			{
				if (roadmap.RoadmapId == roadmapId)
				{
					return true;
				}
			}
			return false;
		}

		return false;
	}

	private async Task<IList<RoadmapVotesModel>> GetAllRoadmapsUserVoted(Guid? loggedInUserId)
	{
		var roadmaps = await _roadmapVotesService.GetAllRoadmapsUserVoted(loggedInUserId);

		return roadmaps!;
	}

	private async Task<Guid> GetRoadmapVoteIdByUserAndRoadmapId(Guid roadmapId, Guid? loggedInUserId)
	{
		if (loggedInUserId == Guid.Empty)
		{
			return Guid.Empty;
		}

		var roadmapVote = await _roadmapVotesService.GetRoadmapVoteIdByUserAndRoadmapId(loggedInUserId, roadmapId);
		var voteId = roadmapVote!.Id;

		return voteId;
	}


	public async Task<string> GetButtonColor(Guid roadmapId, Guid? loggedInUserId)
	{
		var result = await VerifiyIfUserVotedOnRoadmap(roadmapId, loggedInUserId);

		if (result is true)
		{
			return "vote-button-voted";
		}

		return "vote-button";
	}

	public async Task<int> GetRoadmapVotes(Guid roadmapId)
	{
		var roadmapVotes = await _roadmapVotesService.GetRoadmapVotesByRoadmapId(roadmapId);

		if (roadmapVotes is null)
		{
			return 0;
		}

		return roadmapVotes.Count();
	}
}
