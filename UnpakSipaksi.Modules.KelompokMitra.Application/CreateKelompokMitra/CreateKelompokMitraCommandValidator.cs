using FluentValidation;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.CreateKelompokMitra
{
    public sealed class CreateKelompokMitraCommandValidator : AbstractValidator<CreateKelompokMitraCommand>
    {
        public CreateKelompokMitraCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
