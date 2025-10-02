using FluentValidation;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.CreatePeningkatanKeberdayaanMitra
{
    public sealed class CreatePeningkatanKeberdayaanMitraCommandValidator : AbstractValidator<CreatePeningkatanKeberdayaanMitraCommand>
    {
        public CreatePeningkatanKeberdayaanMitraCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
