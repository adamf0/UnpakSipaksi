using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.DeleteMetodeRencanaKegiatan
{
    public sealed class DeleteMetodeRencanaKegiatanCommandValidator : AbstractValidator<DeleteMetodeRencanaKegiatanCommand>
    {
        public DeleteMetodeRencanaKegiatanCommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
