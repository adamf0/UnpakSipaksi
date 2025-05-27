
using FluentValidation;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1
{
    public sealed class CreateRumpunIlmu1CommandValidator : AbstractValidator<CreateRumpunIlmu1Command>
    {
        public CreateRumpunIlmu1CommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
