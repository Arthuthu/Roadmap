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
	public async Task<IList<string>> ValidateUserRegistration(UserModel userData)
	{
		var validationResult = _userValidator.Validate(userData);
		IList<string> validationMessages = new List<string>();

		if (validationResult.IsValid is false)
		{
			foreach (var errors in validationResult.Errors)
			{
				validationMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
			}

			return validationMessages;
		}

		return validationMessages;
	}

	public async Task<IList<string>> ValidateRoadmapCreation(RoadmapClassModel roadmapData)
	{
		var validationResult = _roadmapValidator.Validate(roadmapData);
		IList<string> validationMessages = new List<string>();

		if (validationResult.IsValid is false)
		{
			foreach (var errors in validationResult.Errors)
			{
				validationMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
			}

			return validationMessages;
		}

		return validationMessages;
	}
	public async Task<string> ConcatRegistrationMessages(IList<string> responseMessages)
	{
		string cleanMessage = string.Join(", ", responseMessages);

		return cleanMessage;
	}
}
