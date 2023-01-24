using FluentValidation;
using RoadmapAPIApp.Request;

namespace RoadmapAPIApp.Validators;

public class UserValidator : AbstractValidator<UserRequest>
{
	public UserValidator()
	{
		RuleFor(x => x.Username).NotEmpty().WithMessage("Campo nao pode ser nulo")
			.MaximumLength(50).WithMessage("Os caracteres nao podem ultrapssar 50");

		RuleFor(x => x.Password).NotEmpty().WithMessage("Campo nao pode ser nulo")
			.MinimumLength(5).WithMessage("A senha precisa ter no minimo 5 caracters")
			.MaximumLength(20).WithMessage("A senha nao pode ultrapassar 20 caracteres");
	}
}
