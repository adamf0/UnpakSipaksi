using FluentValidation;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.CreateKualitasKuantitasPublikasiProsiding
{
    public sealed class CreateKualitasKuantitasPublikasiProsidingCommandValidator : AbstractValidator<CreateKualitasKuantitasPublikasiProsidingCommand>
    {
        public CreateKualitasKuantitasPublikasiProsidingCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
               .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
