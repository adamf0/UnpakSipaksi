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
               .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
