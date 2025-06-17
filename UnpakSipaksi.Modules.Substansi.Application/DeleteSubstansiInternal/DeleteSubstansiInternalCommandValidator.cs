using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Substansi.Application.DeleteSubstansiInternal
{
    public sealed class DeleteSubstansiInternalCommandValidator : AbstractValidator<DeleteSubstansiInternalCommand>
    {
        public DeleteSubstansiInternalCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
