using FluentValidation;
using RoadmapRepository.Models;

namespace RoadmapRepository.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
	public UserValidator()
	{
		RuleFor(x => x.Username).NotEmpty().MinimumLength(2);
		RuleFor(x => x.Password).NotEmpty().MinimumLength(2);
	}
}
