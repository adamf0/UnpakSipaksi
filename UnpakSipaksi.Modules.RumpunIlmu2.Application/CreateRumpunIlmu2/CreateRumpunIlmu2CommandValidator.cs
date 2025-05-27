

using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2
{
    public sealed class CreateRumpunIlmu2CommandValidator : AbstractValidator<CreateRumpunIlmu2Command>
    {
        public CreateRumpunIlmu2CommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.UuidRumpunIlmu1)
                .NotEmpty().WithMessage("'RumpunIlmu1' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'RumpunIlmu1' harus dalam format UUID v4 yang valid.");
        }
    }
}
