using RoadmapSite.Models;

namespace RoadmapSite.Services.RoadmapVotes.Classes
{
	public interface IRoadmapVotesService
	{
		Task<string> AddRoadmapVote(RoadmapVotesModel roadmapVote);
		Task<IList<RoadmapVotesModel>> GetAllRoadmaps();
		Task<string> RemoveRoadmapVote();
	}
}