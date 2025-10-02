using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.ApprovalMemberDosen
{
    public sealed class ApprovalMemberDosenCommandValidator : AbstractValidator<ApprovalMemberDosenCommand>
    {
        public ApprovalMemberDosenCommandValidator()
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

            RuleFor(c => c.Status)
               .NotEmpty().WithMessage("'Status' tidak boleh kosong.");
        }
    }
}
