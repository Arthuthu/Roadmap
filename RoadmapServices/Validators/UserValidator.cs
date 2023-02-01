using FluentValidation;
using RoadmapRepository.Models;

namespace RoadmapServices.Validators;

public class UserValidator : AbstractValidator<UserModel>
{
	public UserValidator()
	{
		RuleFor(x => x.Username)
			.NotEmpty().WithMessage("O campo nome precisa ser preenchido")
			.MinimumLength(3).WithMessage("Campo nome precisa ter no minimo 3 caracteres")
			.MaximumLength(50).WithMessage("Os caracteres nao podem ultrapssar 50");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("O campo senha precisa ser preenchido")
			.MinimumLength(5).WithMessage("A senha precisa ter no minimo 5 caracters")
			.MaximumLength(20).WithMessage("A senha nao pode ultrapassar 20 caracteres");
	}
}
