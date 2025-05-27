using FluentValidation;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.CreateKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed class CreateKualitasKuantitasPublikasiJurnalIlmiahCommandValidator : AbstractValidator<CreateKualitasKuantitasPublikasiJurnalIlmiahCommand>
    {
        public CreateKualitasKuantitasPublikasiJurnalIlmiahCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
