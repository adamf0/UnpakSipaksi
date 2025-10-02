using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRumpunIlmu
{
    public sealed class UpdateRumpunIlmuCommandValidator : AbstractValidator<UpdateRumpunIlmuCommand>
    {
        public UpdateRumpunIlmuCommandValidator()
        {
            RuleFor(c => c.UuidPenelitianHibah)
               .NotEmpty().WithMessage("'UuidPenelitianHibah' tidak boleh kosong.")
               .Must(Helper.BeValidGuidV4).WithMessage("'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.");
        }
    }
}
