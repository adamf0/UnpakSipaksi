using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah
{
    public sealed class UpdatePenelitianHibahCommandValidator : AbstractValidator<UpdatePenelitianHibahCommand>
    {
        public UpdatePenelitianHibahCommandValidator()
        {
            RuleFor(c => c.Uuid)
               .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.NIDN)
                .NotEmpty().WithMessage("'NIDN' tidak boleh kosong.")
                .Must(Helper.BeValidNidn).WithMessage("'NIDN' tidak valid.");

            RuleFor(c => c.TahunPengajuan)
               .NotEmpty().WithMessage("'TahunPengajuan' tidak boleh kosong.")
               .Must(Helper.BeValidDate).WithMessage("'TahunPengajuan' harus valid format.");

            RuleFor(c => c.Judul)
               .NotEmpty().WithMessage("'Judul' tidak boleh kosong.");
        }
    }
}
