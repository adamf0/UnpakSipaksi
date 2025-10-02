using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLamaKegiatan
{
    public sealed class UpdateLamaKegiatanCommandValidator : AbstractValidator<UpdateLamaKegiatanCommand>
    {
        public UpdateLamaKegiatanCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.LamaKegiatan)
               .GreaterThanOrEqualTo(0).WithMessage("'LamaKegiatan' tidak boleh negative.");
        }
    }
}
