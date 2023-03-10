using FluentValidation;
using RoadmapRepository.Models;

namespace RoadmapServices.Validators;

public class ComentarioValidator : AbstractValidator<ComentarioModel>
{
    public ComentarioValidator()
    {
		RuleFor(x => x.Comentario)
			.NotEmpty().WithMessage("O campo comentario precisa ser preenchido")
			.MinimumLength(2).WithMessage("Campo nome precisa ter no minimo 2 caracteres")
			.MaximumLength(1000).WithMessage("Os caracteres nao podem ultrapssar 1000 caracteres");
	}
}
