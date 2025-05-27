


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.DeleteRumpunIlmu3
{
    public sealed class DeleteRumpunIlmu3CommandValidator : AbstractValidator<DeleteRumpunIlmu3Command>
    {
        public DeleteRumpunIlmu3CommandValidator()
        {
            RuleFor(c => c.uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
