namespace RoadmapSite.Services.Voting.Interfaces
{
    public interface IVotingService
    {
        Task AddUserVote(Guid roadmapId, Guid? loggedInUserId);
        Task<string> GetButtonColor(Guid roadmapId, Guid? loggedInUserId);
        Task<int> GetRoadmapVotes(Guid roadmapId);
    }
}