using FluentValidation;
using RoadmapAPIApp.Request;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.Validators;

public class NodeValitor : AbstractValidator<NodeRequest>
{
	public NodeValitor()
	{
		RuleFor(x => x.Name)
			.MinimumLength(3).WithMessage("O campo nome deve ter mais que 3 caracteres")
			.MaximumLength(50).WithMessage("O campo nome nao pode ultrapassar 50 caracteres");

		RuleFor(x => x.Description)
			.MaximumLength(200).WithMessage("O campo descricao nao pode ultrapassar 200 caracteres");

		RuleFor(x => x.RoadmapId)
			.NotEmpty().WithMessage("É obrigatorio passar um roadmap valido");
	}
}
