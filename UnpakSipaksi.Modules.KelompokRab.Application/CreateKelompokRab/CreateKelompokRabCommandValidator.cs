using FluentValidation;

namespace UnpakSipaksi.Modules.KelompokRab.Application.CreateKelompokRab
{
    public sealed class CreateKelompokRabCommandValidator : AbstractValidator<CreateKelompokRabCommand>
    {
        public CreateKelompokRabCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
