﻿namespace RoadmapBlazor.Services.Voting;

public interface IRoadmapVotingService
{
    Task AddUserVote(Guid? loggedInUserId, Guid roadmapId);
    Task<string> GetButtonColor(Guid? loggedInUserId, Guid roadmapId);
    Task<int> GetRoadmapVotes(Guid? loggedInUserId, Guid roadmapId);
}