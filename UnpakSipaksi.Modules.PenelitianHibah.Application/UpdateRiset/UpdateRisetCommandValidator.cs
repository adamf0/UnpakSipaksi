using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRiset
{
    public sealed class UpdateRisetCommandValidator : AbstractValidator<UpdateRisetCommand>
    {
        public UpdateRisetCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
               .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");
        }
    }
}
