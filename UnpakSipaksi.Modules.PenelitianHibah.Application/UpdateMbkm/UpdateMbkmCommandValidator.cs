using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMbkm
{
    public sealed class UpdateMbkmCommandValidator : AbstractValidator<UpdateMbkmCommand>
    {
        public UpdateMbkmCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.NPM)
                .NotEmpty().WithMessage("'NPM' tidak boleh kosong.")
                .Must(Helper.BeValidNPM).WithMessage("'NPM' tidak valid.");

            RuleFor(c => c.BuktiMbkm)
                .NotEmpty().WithMessage("'BuktiMbkm' tidak boleh kosong.")
                .Must(e => Helper.BeAValidDriveLink(e, "drive.google.com")).WithMessage("'BuktiMbkm' tidak valid format.");
        }
    }
}
