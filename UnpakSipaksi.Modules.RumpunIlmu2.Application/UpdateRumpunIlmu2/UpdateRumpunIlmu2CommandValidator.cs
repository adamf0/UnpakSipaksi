


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2
{
    public sealed class UpdateRumpunIlmu2CommandValidator : AbstractValidator<UpdateRumpunIlmu2Command>
    {
        public UpdateRumpunIlmu2CommandValidator()
        {
            RuleFor(c => c.Uuid)
                .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.UuidRumpunIlmu1)
                .NotEmpty().WithMessage("'RumpunIlmu1' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'RumpunIlmu1' harus dalam format UUID v4 yang valid.");
        }
    }
}
