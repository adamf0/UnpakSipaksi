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
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
