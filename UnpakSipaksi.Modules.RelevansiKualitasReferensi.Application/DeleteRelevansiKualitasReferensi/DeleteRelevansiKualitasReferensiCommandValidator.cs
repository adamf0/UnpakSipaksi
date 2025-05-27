
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.DeleteRelevansiKualitasReferensi
{
    public sealed class DeleteRelevansiKualitasReferensiCommandValidator : AbstractValidator<DeleteRelevansiKualitasReferensiCommand>
    {
        public DeleteRelevansiKualitasReferensiCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
