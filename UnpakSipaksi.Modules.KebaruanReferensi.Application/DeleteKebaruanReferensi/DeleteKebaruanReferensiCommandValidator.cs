using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.DeleteKebaruanReferensi
{
    public sealed class DeleteKebaruanReferensiCommandValidator : AbstractValidator<DeleteKebaruanReferensiCommand>
    {
        public DeleteKebaruanReferensiCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
