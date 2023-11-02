using Microsoft.AspNetCore.Components;
using Site.Services.RoadmapVotes;

namespace Site.Services.Voting;

public class RoadmapVotingService : IRoadmapVotingService
{
	private readonly NavigationManager _navigationManager;
	private readonly IRoadmapVotesService _roadmapVotesService;

	public RoadmapVotingService(NavigationManager navigationManager,
		IRoadmapVotesService roadmapVotesService)
	{
		_navigationManager = navigationManager;
		_roadmapVotesService = roadmapVotesService;
	}

	public async Task AddUserVote(Guid? loggedInUserId, Guid roadmapId)
	{
		if (loggedInUserId == Guid.Empty)
		{
			_navigationManager.NavigateTo("/login");
		}

		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes(loggedInUserId, roadmapId);

		var votedRoadmapId = roadmapVotes!
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

	public async Task<string> GetButtonColor(Guid? loggedInUserId, Guid roadmapId)
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes(loggedInUserId, roadmapId);

		var votedRoadmapId = roadmapVotes!
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
	public async Task<int> GetRoadmapVotes(Guid? loggedInUserId, Guid roadmapId)
	{
		var roadmapVotes = await _roadmapVotesService.GetAllRoadmapVotes(loggedInUserId, roadmapId);

		if (roadmapVotes is null)
		{
			return 0;
		}

		var roadmapVoteCount = roadmapVotes.Where(x => x.RoadmapId == roadmapId).Count();

		return roadmapVoteCount;
	}
}
