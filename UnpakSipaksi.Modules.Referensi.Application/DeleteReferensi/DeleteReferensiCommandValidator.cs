
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.Referensi.Application.DeleteReferensi
{
    public sealed class DeleteReferensiCommandValidator : AbstractValidator<DeleteReferensiCommand>
    {
        public DeleteReferensiCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
