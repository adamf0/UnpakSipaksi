
using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.DeleteKualitasKuantitasPublikasiProsiding
{
    public sealed class DeleteKualitasKuantitasPublikasiProsidingCommandValidator : AbstractValidator<DeleteKualitasKuantitasPublikasiProsidingCommand>
    {
        public DeleteKualitasKuantitasPublikasiProsidingCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
