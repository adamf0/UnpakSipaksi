using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.DeleteDokumenHibah
{
    public sealed class DeleteDokumenHibahCommandValidator : AbstractValidator<DeleteDokumenHibahCommand>
    {
        public DeleteDokumenHibahCommandValidator()
        {
            RuleFor(c => c.uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
