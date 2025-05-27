
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.DeleteKetajamanAnalisis
{
    public sealed class DeleteKetajamanAnalisisValidator : AbstractValidator<DeleteKetajamanAnalisisCommand>
    {
        public DeleteKetajamanAnalisisValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
