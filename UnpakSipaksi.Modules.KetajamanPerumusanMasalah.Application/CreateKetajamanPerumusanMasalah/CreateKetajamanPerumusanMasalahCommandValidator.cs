using FluentValidation;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah
{
    public sealed class CreateKetajamanPerumusanMasalahCommandValidator : AbstractValidator<CreateKetajamanPerumusanMasalahCommand>
    {
        public CreateKetajamanPerumusanMasalahCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Skor)
                .NotEmpty().WithMessage("'Skor' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Skor' tidak boleh kurang dari 0");
        }
    }
}
