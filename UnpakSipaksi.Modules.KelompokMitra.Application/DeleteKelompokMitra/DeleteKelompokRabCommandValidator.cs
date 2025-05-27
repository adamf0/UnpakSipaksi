using FluentValidation;
using UnpakSipaksi.Common.Application;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.DeleteKelompokMitra
{
    public sealed class DeleteKelompokMitraCommandValidator : AbstractValidator<DeleteKelompokMitraCommand>
    {
        public DeleteKelompokMitraCommandValidator()
        {
            RuleFor(c => c.uuid)
              .NotEmpty().WithMessage("'Uuid' tidak boleh kosong.")
              .Must(Helper.BeValidGuidV4).WithMessage("'Uuid' harus dalam format UUID v4 yang valid.");
        }
    }
}
