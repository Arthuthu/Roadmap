using FluentValidation;
using Roadmap.Domain.Models;

namespace Roadmap.Infra.Validators;

public class RoadmapValidator : AbstractValidator<RoadmapClassModel>
{
    public RoadmapValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo nome precisa ser preenchido")
            .MinimumLength(3).WithMessage("O campo nome precisa ter pelo menos 3 caracteres")
            .MaximumLength(50).WithMessage("O campo nome pode ter no maximo 50 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("O campo descrição pode ter no maximo 200 caracteres");

        RuleFor(x => x.Category)
            .MaximumLength(50).WithMessage("O campo categoria pode ter no maximo 50 caracteres");
    }
}
