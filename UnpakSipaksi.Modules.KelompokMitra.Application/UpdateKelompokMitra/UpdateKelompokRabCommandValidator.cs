using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.UpdateKelompokMitra
{
    public sealed class UpdateKelompokMitraCommandValidator : AbstractValidator<UpdateKelompokMitraCommand>
    {
        public UpdateKelompokMitraCommandValidator()
        {
            RuleFor(c => c.Uuid)
              .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
              .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");

            RuleFor(c => c.Nama)
                .NotEmpty().WithMessage("'Nama' tidak boleh kosong.");
        }
    }
}
