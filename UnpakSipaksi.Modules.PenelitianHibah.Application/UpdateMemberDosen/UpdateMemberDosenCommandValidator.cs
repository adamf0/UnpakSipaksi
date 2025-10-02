using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberDosen
{
    public sealed class UpdateMemberDosenCommandValidator : AbstractValidator<UpdateMemberDosenCommand>
    {
        public UpdateMemberDosenCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");


            RuleFor(c => c.NIDN)
                .NotEmpty().WithMessage("'NIDN' tidak boleh kosong.")
                .Must(Helper.BeValidNidn).WithMessage("'NIDN' tidak valid.");
        }
    }
}
