using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget
{
    public sealed class DeleteKewajaranTahapanTargetCommandValidator : AbstractValidator<DeleteKewajaranTahapanTargetCommand>
    {
        public DeleteKewajaranTahapanTargetCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
