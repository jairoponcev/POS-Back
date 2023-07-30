using FluentValidation;
using POS.Application.Dtos.User.Request;

namespace POS.Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull().WithMessage("El campo nombre de usuario no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre de usuario no puede ser vacío.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("El campo contraseña no puede ser nulo.")
                .NotEmpty().WithMessage("El campo contraseña no puede ser vacío.");
        }
    }
}
