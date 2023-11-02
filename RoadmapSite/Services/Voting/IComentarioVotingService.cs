namespace RoadmapSite.Services.Voting;

public interface IComentarioVotingService
{
    Task AddUserVote(Guid? loggedInUserId, Guid comentarioId);
    Task<string> GetButtonColor(Guid? loggedInUserId, Guid comentarioId);
    Task<int> GetComentarioVotes(Guid? loggedInUserId, Guid comentarioId);
}