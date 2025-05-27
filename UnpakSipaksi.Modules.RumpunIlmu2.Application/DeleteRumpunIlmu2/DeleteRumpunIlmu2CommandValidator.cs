


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2
{
    public sealed class DeleteRumpunIlmu2CommandValidator : AbstractValidator<DeleteRumpunIlmu2Command>
    {
        public DeleteRumpunIlmu2CommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
