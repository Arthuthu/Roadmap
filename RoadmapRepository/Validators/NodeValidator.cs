using FluentValidation;
using RoadmapRepository.Models;

namespace RoadmapRepository.Validators;

public class NodeValidator : AbstractValidator<NodeModel>
{
	public NodeValidator()
	{
		RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(50);
		RuleFor(x => x.Description).MaximumLength(200);
		RuleFor(x => x.RoadmapId).NotEmpty();
	}
}
