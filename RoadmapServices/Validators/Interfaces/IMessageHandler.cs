using RoadmapRepository.Models;

namespace RoadmapServices.Validators.Interfaces
{
    public interface IMessageHandler
    {
        Task<IList<string>> ValidateRoadmapCreation(RoadmapClassModel roadmapData);
        Task<IList<string>> ValidateUserRegistration(UserModel userData);
        Task<string> ConcatRegistrationMessages(IList<string> responseMessages);
	}
}