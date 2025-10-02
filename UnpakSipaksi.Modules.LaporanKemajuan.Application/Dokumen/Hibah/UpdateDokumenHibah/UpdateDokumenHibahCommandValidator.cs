using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.UpdateDokumenHibah
{
    public sealed class UpdateDokumenHibahCommandValidator : AbstractValidator<UpdateDokumenHibahCommand>
    {
        public UpdateDokumenHibahCommandValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPenenitianHibah)
                .NotEmpty().WithMessage("'Hibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Hibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.File)
                .NotEmpty().WithMessage("'File' tidak boleh kosong.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("'Type' tidak boleh kosong.");
        }
    }
}
