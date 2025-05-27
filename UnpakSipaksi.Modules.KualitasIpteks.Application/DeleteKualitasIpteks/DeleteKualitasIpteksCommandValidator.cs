using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.DeleteKualitasIpteks
{
    public sealed class DeleteKualitasIpteksCommandValidator : AbstractValidator<DeleteKualitasIpteksCommand>
    {
        public DeleteKualitasIpteksCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
