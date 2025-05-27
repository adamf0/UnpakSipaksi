

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1
{
    public sealed class DeleteRumpunIlmu1CommandValidator : AbstractValidator<DeleteRumpunIlmu1Command>
    {
        public DeleteRumpunIlmu1CommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
