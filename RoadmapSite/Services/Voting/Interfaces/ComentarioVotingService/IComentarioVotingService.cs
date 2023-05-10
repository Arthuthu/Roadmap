namespace RoadmapSite.Services.Voting.Interfaces.ComentarioVotingService
{
    public interface IComentarioVotingService
    {
        Task AddUserVote(Guid comentarioId, Guid? loggedInUserId);
        Task<string> GetButtonColor(Guid comentarioId, Guid? loggedInUserId);
        Task<int> GetComentarioVotes(Guid comentarioId, Guid? loggedInUserId);
    }
}