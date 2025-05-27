using FluentValidation;

namespace UnpakSipaksi.Modules.Rirn.Application.CreateRirn
{
    public sealed class CreateRirnCommandValidator : AbstractValidator<CreateRirnCommand>
    {
        public CreateRirnCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
