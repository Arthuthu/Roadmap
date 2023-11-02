using Domain.Models;
using FluentValidation;
using Infra.Validators.Interfaces;

namespace Infra.Validators;
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
	public IList<string>? ValidateUserRegistration(UserModel userData)
	{
		var validationResult = _userValidator.Validate(userData);

		if (!validationResult.IsValid)
		{
			return validationResult.Errors.Select(e => e.ErrorMessage).ToList();
		}

		return null;
	}

	public IList<string>? ValidateRoadmapCreation(RoadmapClassModel roadmapData)
	{
		var validationResult = _roadmapValidator.Validate(roadmapData);

		if (!validationResult.IsValid)
		{
			return validationResult.Errors.Select(e => e.ErrorMessage).ToList();
		}

		return null;
	}

	public string ConcatRegistrationMessages(IList<string> responseMessages)
	{
		string cleanMessage = string.Join(", ", responseMessages);

		return cleanMessage;
	}
}
