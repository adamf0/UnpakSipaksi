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
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
