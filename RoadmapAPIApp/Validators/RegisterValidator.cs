using FluentValidation;
using RoadmapAPIApp.Request;

namespace RoadmapAPIApp.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
	public RegisterValidator()
	{
		RuleFor(x => x.Username)
			.MinimumLength(3).WithMessage("Campo nome precisa ter no minimo 3 caracteres")
			.MaximumLength(50).WithMessage("Os caracteres nao podem ultrapssar 50");

		RuleFor(x => x.Password)
			.MinimumLength(5).WithMessage("A senha precisa ter no minimo 5 caracters")
			.MaximumLength(20).WithMessage("A senha nao pode ultrapassar 20 caracteres");
	}
}
