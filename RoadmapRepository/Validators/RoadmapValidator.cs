using FluentValidation;
using RoadmapRepository.Models;

namespace RoadmapRepository.Validators;

public class RoadmapValidator : AbstractValidator<RoadmapModel>
{
	public RoadmapValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(50);
		RuleFor(x => x.Description).MaximumLength(200);
		RuleFor(x => x.UserId).NotEmpty();
	}
}
