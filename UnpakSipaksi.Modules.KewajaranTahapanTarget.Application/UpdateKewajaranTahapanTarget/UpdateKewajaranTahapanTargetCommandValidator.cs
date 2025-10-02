using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKewajaranTahapanTarget
{
    public sealed class UpdateKewajaranTahapanTargetCommandValidator : AbstractValidator<UpdateKewajaranTahapanTargetCommand>
    {
        public UpdateKewajaranTahapanTargetCommandValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.Nilai)
              .GreaterThanOrEqualTo(0).WithMessage("'Nilai' tidak boleh negative.");
        }
    }
}
