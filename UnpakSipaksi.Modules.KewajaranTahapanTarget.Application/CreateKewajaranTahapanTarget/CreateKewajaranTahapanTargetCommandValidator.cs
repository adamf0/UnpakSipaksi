using FluentValidation;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKewajaranTahapanTarget
{
    public sealed class CreateKewajaranTahapanTargetCommandValidator : AbstractValidator<CreateKewajaranTahapanTargetCommand>
    {
        public CreateKewajaranTahapanTargetCommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
                .NotEmpty().WithMessage("'Nilai' tidak boleh kosong.")
                .LessThan(0).WithMessage("'Nilai' tidak boleh kurang dari 0");
        }
    }
}
