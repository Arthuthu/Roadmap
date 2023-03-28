namespace RoadmapSite.Services.Voting.Interfaces.RoadmapVotingService
{
    public interface IRoadmapVotingService
    {
        Task AddUserVote(Guid roadmapId, Guid? loggedInUserId);
        Task<string> GetButtonColor(Guid roadmapId, Guid? loggedInUserId);
        Task<int> GetRoadmapVotes(Guid roadmapId);

    }
}