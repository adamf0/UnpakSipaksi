using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.DeleteKetajamanPerumusanMasalah
{
    public sealed class DeleteKetajamanPerumusanMasalahCommandValidator : AbstractValidator<DeleteKetajamanPerumusanMasalahCommand>
    {
        public DeleteKetajamanPerumusanMasalahCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
