


using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.CreateRumpunIlmu3
{
    public sealed class CreateRumpunIlmu3CommandValidator : AbstractValidator<CreateRumpunIlmu3Command>
    {
        public CreateRumpunIlmu3CommandValidator()
        {
            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");

            RuleFor(c => c.UuidRumpunIlmu2)
                .NotEmpty().WithMessage("'RumpunIlmu2' tidak boleh kosong.")
                .Must(Helper.BeValidGuidV4).WithMessage("'RumpunIlmu2' harus dalam format UUID v4 yang valid.");
        }
    }
}
