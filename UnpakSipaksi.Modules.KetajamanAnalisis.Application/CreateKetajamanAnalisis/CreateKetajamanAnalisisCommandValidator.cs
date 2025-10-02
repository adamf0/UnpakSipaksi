using FluentValidation;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.CreateKetajamanAnalisis
{
    public sealed class CreateKetajamanAnalisisValidator : AbstractValidator<CreateKetajamanAnalisisCommand>
    {
        public CreateKetajamanAnalisisValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
              .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
