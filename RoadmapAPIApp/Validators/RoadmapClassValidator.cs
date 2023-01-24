using FluentValidation;
using RoadmapAPIApp.Request;
using RoadmapRepository.Models;

namespace RoadmapAPIApp.Validators;

public class RoadmapClassValidator : AbstractValidator<RoadmapClassRequest>
{
	public RoadmapClassValidator()
	{
		RuleFor(x => x.Name)
			.MinimumLength(1).WithMessage("O campo nome deve ter mais que 3 caracters")
			.MaximumLength(50).WithMessage("O campo nome nao pode ultrapassar 50 caracteres");

		RuleFor(x => x.Description)
			.MaximumLength(200).WithMessage("O campo descricao nao pode ultrapassar 200 caracters");

		RuleFor(x => x.UserId)
			.NotEmpty().WithMessage("E obrigatorio passar um usuario valido");
	}
}
