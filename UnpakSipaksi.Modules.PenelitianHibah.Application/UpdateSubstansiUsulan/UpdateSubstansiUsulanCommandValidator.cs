using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSubstansiUsulan
{
    public sealed class UpdateSubstansiUsulanCommandValidator : AbstractValidator<UpdateSubstansiUsulanCommand>
    {
        public UpdateSubstansiUsulanCommandValidator()
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
