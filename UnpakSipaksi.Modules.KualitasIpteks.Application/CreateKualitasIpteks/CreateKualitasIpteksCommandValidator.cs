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
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
