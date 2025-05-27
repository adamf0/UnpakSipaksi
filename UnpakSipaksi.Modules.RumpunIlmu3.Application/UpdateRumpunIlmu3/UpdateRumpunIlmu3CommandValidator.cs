


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.UpdateRumpunIlmu3
{
    public sealed class UpdateRumpunIlmu3CommandValidator : AbstractValidator<UpdateRumpunIlmu3Command>
    {
        public UpdateRumpunIlmu3CommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.UuidRumpunIlmu2)
                .NotEmpty().WithMessage("'RumpunIlmu2' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'RumpunIlmu2' harus dalam format UUID v4 yang valid.");
        }
    }
}
