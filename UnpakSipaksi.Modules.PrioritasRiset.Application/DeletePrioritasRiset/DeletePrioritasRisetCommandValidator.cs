
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.DeletePrioritasRiset
{
    public sealed class DeletePrioritasRisetCommandValidator : AbstractValidator<DeletePrioritasRisetCommand>
    {
        public DeletePrioritasRisetCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
