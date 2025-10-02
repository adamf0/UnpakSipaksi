using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreatePenelitianHibah
{
    public sealed class CreatePenelitianHibahCommandValidator : AbstractValidator<CreatePenelitianHibahCommand>
    {
        public CreatePenelitianHibahCommandValidator()
        {
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
