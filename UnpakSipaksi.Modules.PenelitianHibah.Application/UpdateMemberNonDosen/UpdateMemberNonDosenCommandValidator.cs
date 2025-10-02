using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberNonDosen
{
    public sealed class UpdateMemberNonDosenCommandValidator : AbstractValidator<UpdateMemberNonDosenCommand>
    {
        public UpdateMemberNonDosenCommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");
        }
    }
}
