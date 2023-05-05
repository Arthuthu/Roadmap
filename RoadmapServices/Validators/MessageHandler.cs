using FluentValidation;
using RoadmapRepository.Models;
using RoadmapServices.Validators.Interfaces;

namespace RoadmapServices.Validators;
public class MessageHandler : IMessageHandler
{
	private readonly IValidator<RoadmapClassModel> _roadmapValidator;
	private readonly IValidator<UserModel> _userValidator;

	public MessageHandler(IValidator<RoadmapClassModel> roadmapValidator,
		IValidator<UserModel> userValidator)
	{
		_roadmapValidator = roadmapValidator;
		_userValidator = userValidator;
	}
	public IList<string> ValidateUserRegistration(UserModel userData)
	{
		var validationResult = _userValidator.Validate(userData);

		if (!validationResult.IsValid)
		{
			return validationResult.Errors.Select(e => e.ErrorMessage).ToList();
		}

		return new List<string>();
	}

	public IList<string> ValidateRoadmapCreation(RoadmapClassModel roadmapData)
	{
		var validationResult = _roadmapValidator.Validate(roadmapData);

		if (!validationResult.IsValid)
		{
			return validationResult.Errors.Select(e => e.ErrorMessage).ToList();
		}

		return new List<string>();
	}

	public string ConcatRegistrationMessages(IList<string> responseMessages)
	{
		string cleanMessage = string.Join(", ", responseMessages);

		return cleanMessage;
	}
}
