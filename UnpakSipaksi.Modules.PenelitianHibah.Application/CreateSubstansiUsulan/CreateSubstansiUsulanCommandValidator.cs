using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateSubstansiUsulan
{
    public sealed class CreateSubstansiUsulanCommandValidator : AbstractValidator<CreateSubstansiUsulanCommand>
    {
        public CreateSubstansiUsulanCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
                .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.File)
               .NotEmpty().WithMessage("'File' tidak boleh kosong.");
        }
    }
}
