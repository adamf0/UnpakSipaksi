using FluentValidation;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.CreateKualitasIpteks
{
    public sealed class CreateKualitasIpteksCommandValidator : AbstractValidator<CreateKualitasIpteksCommand>
    {
        public CreateKualitasIpteksCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
