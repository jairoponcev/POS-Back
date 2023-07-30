using FluentValidation;
using POS.Application.Dtos.Provider.Request;

namespace POS.Application.Validators.Provider
{
    public class ProviderValidator : AbstractValidator<ProviderRequestDto>
    {
        public ProviderValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("El campo email no puede ser nulo.")
                .NotEmpty().WithMessage("El campo email no puede ser vacío.");

            RuleFor(x => x.DocumentTypeId)
                .NotNull().WithMessage("El campo tipo de documento no puede ser nulo.")
                .NotEmpty().WithMessage("El campo tipo de documento no puede ser vacío.");

            RuleFor(x => x.DocumentNumber)
                .NotNull().WithMessage("El campo número de documento no puede ser nulo.")
                .NotEmpty().WithMessage("El campo número de documento no puede ser vacío.");

            RuleFor(x => x.Phone)
                .NotNull().WithMessage("El campo teléfono no puede ser nulo.")
                .NotEmpty().WithMessage("El campo teléfono no puede ser vacío.");

            RuleFor(x => x.State)
                .NotNull().WithMessage("El campo estado no puede ser nulo.")
                .NotEmpty().WithMessage("El campo estado no puede ser vacío.");
        }
    }
}
